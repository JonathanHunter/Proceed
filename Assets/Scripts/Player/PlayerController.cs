using UnityEngine;
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
        private float jumpForce = 13.5f;
        [SerializeField]
        private float moveForce = 200f;
        [SerializeField]
        private float maxSpeed = 4f;
        [SerializeField]
        private float maxKnockbackSpeed = 4f;
        [SerializeField]
        private float airControl = 0.3f;

        private static bool doOnce = false;
        private static bool jump = false;


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

        void Awake()
        {
            //state machine init
            machine = new PlayerStateMachine();
            doState = new state[] { Idle, Moving, InAir, Jump, Attack, Hit, Dead };
            invunTimer = 0;
        }

        void Update()
        {
            bool move = false;
            if (CustomInput.Bool(CustomInput.UserInput.Up) || CustomInput.Bool(CustomInput.UserInput.Down) || CustomInput.Bool(CustomInput.UserInput.Left) || CustomInput.Bool(CustomInput.UserInput.Right))
                move = true;
            TouchingSomething();
            //get next state
            currState = machine.update(inAir, move, hit, animDone);
            if (invunTimer > 0)
            {
                hit = false;
                invunTimer -= Time.deltaTime;
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
                //todo stuff
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.tag == "Level end")
            {
                //todo stuff
            }
        }

        public void AnimDetector()
        {
            animDone = true;
        }

        //detects if you are in the air
        private void TouchingSomething()
        {
            RaycastHit temp;
            if (Physics.Raycast(foot.position, -this.transform.up, out temp, .5f))
                inAir = false;
            inAir = true;
        }

        //fixed update runs on a timed cycle (for physics stuff)
        void FixedUpdate()
        {
            if (currState == Enums.PlayerState.Moving ||
                currState == Enums.PlayerState.InAir )
            {
                //the following logic will accuratly rotate the player to the direction they want to go
                float up = CustomInput.Bool(CustomInput.UserInput.Up) ? CustomInput.Raw(CustomInput.UserInput.Up) : CustomInput.Raw(CustomInput.UserInput.Down);
                float right = CustomInput.Bool(CustomInput.UserInput.Right) ? CustomInput.Raw(CustomInput.UserInput.Right) : CustomInput.Raw(CustomInput.UserInput.Left);
                if (CustomInput.Bool(CustomInput.UserInput.Left) && right > 0)
                    right *= -1;
                if (CustomInput.Bool(CustomInput.UserInput.Down) && up > 0)
                    up *= -1;

                float magnitude = Mathf.Sqrt(up * up + right * right);
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
                //TODO:movement logic
            }
            //STATE MACHINE SAY JUMP NOW!!!
            if (jump)
            {
                //TODO: jump logic
            }
            if (currState == Enums.PlayerState.Hit)
            {
                //TODO: knockback logic
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
                //TODO take damage
                damage = 0;
                doOnce = true;
                invunTimer = invunTime;
                invun = true;
            }
        }

        private static void Dead()
        {
        }
    }
}