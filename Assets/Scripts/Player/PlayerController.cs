//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using System;
using Assets.Scripts.Util;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private Transform foot;
        [SerializeField]
        private AudioSource soundPlayer;
        [SerializeField]
        private emitterController emitter;
        [SerializeField]
        private float jumpForce = 13.5f;
        [SerializeField]
        private float moveForce = 20f;
        [SerializeField]
        private float maxSpeed = 4f;
        [SerializeField]
        private float maxKnockbackSpeed = 4f;
        [SerializeField]
        private float airControl = 0.7f;
        [SerializeField]
        private AudioClip[] step1;
        [SerializeField]
        private AudioClip[] step2;
        [SerializeField]
        private AudioClip[] landing;

        private static bool doOnce = false;
        private static bool jump = false;
        private static bool knockBack = false;

        private static int health = 5;

        private enum BlockMaterial { Wood, Ice, Sand };
        private BlockMaterial curMat;
        private float magnitude;

        //state machine vars
        private static bool invun = false;
        private static float invunTime = .5f;
        private static float invunTimer = 0;
        private static int damage = 0;
        private bool animDone = false;
        private bool inAir = false;
        private bool hit = false;
        private bool render = false;
        private PlayerStateMachine machine;
        private delegate void state();
        private state[] doState;
        private Enums.PlayerState prevState = 0;
        private Enums.PlayerState currState = 0;
        private Rigidbody rgbdy;
        private bool move = false;

        //Death Variables
        private float respawnTimerReset = 0;
        public float respawnTimer = 3f;
        public ragdollBody ragdoll;
        public ragdollBody tempRag;
        private bool ragdollIsActive = false;
        private Vector3 originalScale;

        //Attack Variables
        public EntityBehavior.hitbox hitboxPrefab;

        public bool dead = false;

        // Status Effects
        private bool sluggish;

        void Awake()
        {
            magnitude = 0f;
            curMat = BlockMaterial.Wood;
            respawnTimerReset = respawnTimer;
            originalScale = this.transform.localScale;
            rgbdy = this.gameObject.GetComponent<Rigidbody>();
            //state machine init
            machine = new PlayerStateMachine();
            doState = new state[] { Idle, Moving, InAir, Jump, Attack, Hit, Dead };
            invunTimer = 0;
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.U))
            {
                die();
            }

            #region StatusEffects
            if (sluggish)
            {
                anim.speed = 0.6f;
            }
            else
                anim.speed = 1;
            #endregion
            anim.SetFloat("speed", magnitude);
            if (health <= 0 || this.transform.position.y < -20)
            {
                //Create a ragdoll until we respawn
                if (respawnTimer > 0)
                {
                    if (!ragdollIsActive)
                    {
                        die();
                        //this.GetComponent<Rigidbody>().useGravity = false;
                    }
                    //this.gameObject.transform.position = new Vector3(tempRag.HipN.position.x+1,tempRag.HipN.position.y, tempRag.HipN.position.z + 1);
                    respawnTimer -= Time.deltaTime;
                }
                else
                {
                    //respawn
                    gameObject.transform.localScale = originalScale;
                    ragdollIsActive = false;
                    respawnTimer = respawnTimerReset;
                    health = 5;
                    transform.position = new Vector3(0, 0, 0);
                    FindObjectOfType<GameState>().playerDeaths++;
                    //this.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            move = false;

            if (CustomInput.Bool(CustomInput.UserInput.Up) || CustomInput.Bool(CustomInput.UserInput.Down) || CustomInput.Bool(CustomInput.UserInput.Left) || CustomInput.Bool(CustomInput.UserInput.Right))
                move = true;
            TouchingSomething();
            if ((int)currState == 3 && !inAir)
            {
                jump = true;
            }
            //get next state
            currState = machine.update(inAir, move, hit, animDone);
            if (invunTimer > 0)
            {
                hit = false;
                invunTimer -= Time.deltaTime;
                invun = true;
            }
            else
            {
                invun = false;
            }
            if (dead)
                dead = false;
            //run state
            doState[(int)currState]();
            if (health <= 0)
                die();
            //state clean up
            if (prevState != currState)
            {
                doOnce = false;
                animDone = false;
                jump = false;
                hit = false;
                anim.SetInteger("state", (int)currState);
            }
            prevState = currState;
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "enemy")
            {
                if (!invun)
                    hit = true;
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Level end")
            {
                if (sluggish)
                {
                    sluggish = false;
                }
                this.transform.parent = null;
                ProceduralGen.LevelGenerator level = FindObjectOfType<ProceduralGen.LevelGenerator>();
                level.EndLevel();
                level.StartLevel();
            }
            else if (col.gameObject.tag == "Sand")
            {
                sluggish = true;
            }
        }

        void OnTriggerStay(Collider col)
        {
            if (col.gameObject.CompareTag("Sand"))
            {
                sluggish = true;
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Sand"))
            {
                sluggish = false;
            }
        }

        public void AnimDetector()
        {
            animDone = true;
        }

        public void Step1()
        {
            emitter.emitStep();
            soundPlayer.PlayOneShot(step1[(int)curMat], magnitude);
        }

        public void Step2()
        {
            emitter.emitStep();
            soundPlayer.PlayOneShot(step2[(int)curMat], magnitude);
        }

        //detects if you are in the air
        private void TouchingSomething()
        {
            int playerLayerMask = LayerMask.GetMask("Ignore Raycast", "Player");
            //Invert the layer mask to check all but the player's layer:
            playerLayerMask = ~playerLayerMask;
            RaycastHit temp;
            if (Physics.Raycast(new Vector3(foot.position.x, foot.position.y + 0.2f, foot.position.z), new Vector3(0, -1, 0), out temp, .2f, playerLayerMask))
            {
                Debug.DrawRay(new Vector3(foot.position.x, foot.position.y + 0.2f, foot.position.z), new Vector3(0, .2f, 0), Color.red);
                this.transform.parent = temp.collider.transform.parent;
                if (temp.collider.gameObject.tag == "Wood" || temp.collider.gameObject.tag == "Ice" || temp.collider.gameObject.tag == "Sand")
                    curMat = (BlockMaterial)Enum.Parse(typeof(BlockMaterial), temp.collider.gameObject.tag);
                else
                    curMat = BlockMaterial.Wood;
                if (inAir)
                    soundPlayer.PlayOneShot(landing[(int)curMat]);
                inAir = false;
            }
            else
            {
                this.transform.parent = null;
                inAir = true;
            }
        }

        //fixed update runs on a timed cycle (for physics stuff)
        void FixedUpdate()
        {
            if (!ragdollIsActive)
            {
                rgbdy.AddForce(2 * Physics.gravity * rgbdy.mass);
                if (currState == Enums.PlayerState.Moving ||
                    currState == Enums.PlayerState.InAir || currState == Enums.PlayerState.Jump)
                {
                    //the following logic will accuratly rotate the player to the direction they want to go
                    float up = CustomInput.Bool(CustomInput.UserInput.Up) ? CustomInput.Raw(CustomInput.UserInput.Up) : CustomInput.Raw(CustomInput.UserInput.Down);
                    float right = CustomInput.Bool(CustomInput.UserInput.Right) ? CustomInput.Raw(CustomInput.UserInput.Right) : CustomInput.Raw(CustomInput.UserInput.Left);
                    magnitude = new Vector2(up, right).magnitude;
                    if (magnitude > 1)
                        magnitude = 1;
                    if (up == 0 && right == 0)
                    {

                    }
                    else if (up == 0)
                    {
                        if (CustomInput.Bool(CustomInput.UserInput.Left))
                            transform.rotation = Quaternion.Euler(0, 270, 0);
                        else
                            transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (right == 0)
                    {
                        if (CustomInput.Bool(CustomInput.UserInput.Down))
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        else
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        if (CustomInput.Bool(CustomInput.UserInput.Down))
                            transform.rotation = Quaternion.Euler(0, 180 + Mathf.Rad2Deg * Mathf.Atan(right / up), 0);
                        else
                            transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan(right / up), 0);
                    }

                    if (currState == Enums.PlayerState.Moving)
                    {
                        if (sluggish)
                        {
                            rgbdy.AddForce(-this.transform.right * 0.6f * moveForce * magnitude);
                            if (rgbdy.velocity.x > 0.6f * maxSpeed)
                                rgbdy.velocity = new Vector3(0.6f * maxSpeed, rgbdy.velocity.y, rgbdy.velocity.z);
                            if (rgbdy.velocity.z > 0.6f * maxSpeed)
                                rgbdy.velocity = new Vector3(rgbdy.velocity.x, rgbdy.velocity.y, 0.6f * maxSpeed);
                        }
                        else
                        {
                            rgbdy.AddForce(-this.transform.right * moveForce * magnitude);
                            if (rgbdy.velocity.x > maxSpeed)
                                rgbdy.velocity = new Vector3(maxSpeed, rgbdy.velocity.y, rgbdy.velocity.z);
                            if (rgbdy.velocity.z > maxSpeed)
                                rgbdy.velocity = new Vector3(rgbdy.velocity.x, rgbdy.velocity.y, maxSpeed);
                        }
                    }
                    if ((currState == Enums.PlayerState.InAir || currState == Enums.PlayerState.Jump) && move)
                    {
                        rgbdy.AddForce(-this.transform.right * moveForce * airControl * magnitude, ForceMode.Acceleration);
                        if (rgbdy.velocity.x > maxSpeed)
                            rgbdy.velocity = new Vector3(maxSpeed, rgbdy.velocity.y, rgbdy.velocity.z);
                    }
                }
                //if (!inAir)
                //    rgbdy.velocity = new Vector3(rgbdy.velocity.x, 0, rgbdy.velocity.z);
                if (jump)
                {
                    rgbdy.AddForce(this.transform.up * jumpForce, ForceMode.Impulse);
                    jump = false;
                }
                if (knockBack)
                {
                    rgbdy.AddForce(this.transform.right * maxKnockbackSpeed, ForceMode.Impulse);
                    knockBack = false;
                }
            }
        }

        private static void Idle()
        {
        }

        private static void Attack()
        {
            if (!doOnce)
            {
                doOnce = true;
                //TODO: attack logic
            }
        }

        public void generateHitbox(float knockback)
        {
            EntityBehavior.hitbox temp = GameObject.Instantiate<EntityBehavior.hitbox>(hitboxPrefab);
            temp.transform.position = transform.position;
            temp.knockback = knockback;

        }

        private static void Moving()
        {
        }
        private static void Jump()
        {
            if (!doOnce)
            {
                doOnce = true;
                jump = true;
            }
        }
        private static void InAir()
        {
        }

        private static void Hit()
        {
            if (!doOnce)
            {
                health--;
                doOnce = true;
                invunTimer = invunTime;
                invun = true;
                knockBack = true;
            }
        }

        private static void Dead()
        {

        }

        public void die()
        {
            dead = true;
            health = 0;
            gameObject.transform.localScale = new Vector3(.001f, .001f, .001f);
            if (!ragdollIsActive)
            {
                ragdollIsActive = true;
                tempRag = GameObject.Instantiate<ragdollBody>(ragdoll);
                tempRag.transform.position = this.transform.position;
                tempRag.destructionTimer = respawnTimer; //We set this so the ragdoll only exists as long as we're dead
            }
        }
    }
}