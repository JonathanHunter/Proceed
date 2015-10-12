using UnityEngine;
using Assets.Scripts.Util;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private Transform foot;
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

        private static bool doOnce = false;
        private static bool jump = false;
        private static bool knockBack = false;

        private static int health = 5;


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
        
        // Gameplay Status (sluggish, poisoned, super speed, invincible)
        bool sluggish;

        // Miscellenious 
        private Image UnderWaterGUI;

        void Awake()
        {
            rgbdy = this.gameObject.GetComponent<Rigidbody>();
            //state machine init
            machine = new PlayerStateMachine();
            doState = new state[] { Idle, Moving, InAir, Jump, Attack, Hit, Dead };
            invunTimer = 0;
        }

        void Update()
        {
            float up = CustomInput.Bool(CustomInput.UserInput.Up) ? CustomInput.Raw(CustomInput.UserInput.Up) : CustomInput.Raw(CustomInput.UserInput.Down);
            float right = CustomInput.Bool(CustomInput.UserInput.Right) ? CustomInput.Raw(CustomInput.UserInput.Right) : CustomInput.Raw(CustomInput.UserInput.Left);
            float magnitude = new Vector2(up, right).magnitude;

            #region StatusEffects
            if(sluggish)
            {
                anim.speed = 0.6f;
            }
            #endregion
            anim.SetFloat("speed", magnitude);

            if (health <= 0 || this.transform.position.y < -20)
            {
                health = 5;
                transform.position = new Vector3(0, 0, 0);
                FindObjectOfType<GameState>().playerDeaths++;
            }
            move = false;
            if (magnitude !=0)
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

            //run state
            doState[(int)currState]();
            //state clean up
            if (prevState != currState)
            {
                doOnce = false;
                animDone = false; ;
                jump = false;
                hit = false;
                anim.SetInteger("state", (int)currState);


            }
            prevState = currState;
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "enemy" && !invun)
            {
                if(!invun)
                    hit = true;
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.tag == "Level end")
            {
                // Reset Status
                if (sluggish)
                {
                    sluggish = false;
                    UnderWaterGUI.enabled = false;
                }
                this.transform.parent = null;
                ProceduralGen.LevelGenerator level = FindObjectOfType<ProceduralGen.LevelGenerator>();
                level.EndLevel();
                level.StartLevel();
               
            }else if(col.gameObject.CompareTag("Water"))
            {
                UnderWaterGUI = FindObjectOfType<Image>();
            }
        }

        void OnTriggerStay(Collider col)
        {
            if(col.gameObject.CompareTag("Water"))
            {
                sluggish = true;
                UnderWaterGUI.enabled = true;
            }
        }
        public void AnimDetector()
        {
            animDone = true;
        }

        //detects if you are in the air
        private void TouchingSomething()
        {
            int playerLayerMask = 1 << 8;
            //Invert the layer mask to check all but the player's layer:
            playerLayerMask = ~playerLayerMask;
            RaycastHit temp;
            if (Physics.Raycast(new Vector3(foot.position.x, foot.position.y + 0.2f, foot.position.z), new Vector3(0, -1, 0), out temp, .2f, playerLayerMask))
            {
                inAir = false;
                Debug.DrawRay(new Vector3(foot.position.x, foot.position.y + 0.2f, foot.position.z), new Vector3(0, .2f, 0), Color.red);
                this.transform.parent = temp.collider.transform.parent;
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
            rgbdy.AddForce(2 * Physics.gravity * rgbdy.mass);
            if (currState == Enums.PlayerState.Moving ||
                currState == Enums.PlayerState.InAir || currState == Enums.PlayerState.Jump)
            {
                //the following logic will accuratly rotate the player to the direction they want to go
                float up = CustomInput.Bool(CustomInput.UserInput.Up) ? CustomInput.Raw(CustomInput.UserInput.Up) : CustomInput.Raw(CustomInput.UserInput.Down);
                float right = CustomInput.Bool(CustomInput.UserInput.Right) ? CustomInput.Raw(CustomInput.UserInput.Right) : CustomInput.Raw(CustomInput.UserInput.Left);
                float magnitude = new Vector2(up, right).magnitude;
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

                if(currState == Enums.PlayerState.Moving)
                {
                    if (sluggish)
                    {
                        rgbdy.AddForce(-this.transform.right * (0.6f) * moveForce * magnitude);
                        if (rgbdy.velocity.x > (0.6f) * maxSpeed)
                            rgbdy.velocity = new Vector3((0.6f) * maxSpeed, rgbdy.velocity.y, rgbdy.velocity.z);
                        if (rgbdy.velocity.z > maxSpeed)
                            rgbdy.velocity = new Vector3(rgbdy.velocity.x, rgbdy.velocity.y, (0.6f) * maxSpeed);
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
                if((currState == Enums.PlayerState.InAir || currState == Enums.PlayerState.Jump) && move)
                {
                    if (sluggish)
                    {
                        rgbdy.AddForce(-this.transform.right * (0.6f) * moveForce * airControl * magnitude, ForceMode.Acceleration);
                        if (rgbdy.velocity.x > (0.6f)*maxSpeed)
                            rgbdy.velocity = new Vector3((0.6f)*maxSpeed, rgbdy.velocity.y, rgbdy.velocity.z);
                    }
                    else
                    {
                        rgbdy.AddForce(-this.transform.right * moveForce * airControl * magnitude, ForceMode.Acceleration);
                        if (rgbdy.velocity.x > maxSpeed)
                            rgbdy.velocity = new Vector3(maxSpeed, rgbdy.velocity.y, rgbdy.velocity.z);
                    }
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
    }
}