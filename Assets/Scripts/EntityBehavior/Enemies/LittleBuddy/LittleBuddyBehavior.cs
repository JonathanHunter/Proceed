//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.EntityBehavior
{
    class LittleBuddyBehavior : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private GameObject attack;
        [SerializeField]
        private Transform attackPos;
        [SerializeField]
        private Transform[] wayPoints;
        [SerializeField]
        private int startHealth;
        private float aggresionRadius = 500;
        [SerializeField]
        private float fleeDistance;
        [SerializeField]
        private float timeCanChase;
        private float waitTime = 2;
        [SerializeField]
        private float restTime;
        [SerializeField]
        private float fov;
        [SerializeField]
        private float sightDistance;
        [SerializeField]
        private bool coward;
        [SerializeField]
        private bool fearless;
        [SerializeField]
        private float whiskerLength = 4.5f;
        [SerializeField]
        private float emergencyWhiskerLength = 4f;
        [SerializeField]
        private float runSpeed = 0.1f;
        [SerializeField]
        private float backPedalSpeed = 0.03f;
        [SerializeField]
        private float maxTurnSpeed = 5f;

        private float followDistance = 1f;

        private Player.PlayerController player;
        private bool doOnce, animDone, hit, backOff;
        private int state, prevState, health, navObjectMask, currentNode, backUpDuration, backUpTimer;
        private LittleBuddyStateMachine machine;
        private Vector3 moveDirection;
        private GameObject attackInstance;

        void Start()
        {
            player = FindObjectOfType<Player.PlayerController>();
            state = 0;
            prevState = 0;
            health = startHealth;
            backUpDuration = 30;
            backUpTimer = 31;
            doOnce = false;
            animDone = false;
            backOff = false;
            currentNode = 0;
            navObjectMask = 1 << 10;
            machine = new LittleBuddyStateMachine(aggresionRadius, fleeDistance, timeCanChase, waitTime, restTime, coward, fearless, followDistance);
        }

        void Update()
        {
            if (Util.GameState.paused)
                return;
            if (state != prevState)
            {
                doOnce = false;
                prevState = state;
                anim.SetInteger("state", state);
                if (attackInstance != null)
                    Destroy(attackInstance.gameObject);
            }
            Vector3 dirTowardsPlayer = player.transform.position - transform.position;
            float angle = Vector3.Angle(dirTowardsPlayer, transform.forward);
            angle = angle > 180f ? angle - 360f : angle;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            RaycastHit[] obstacles = Physics.RaycastAll(transform.position, dirTowardsPlayer, distance, navObjectMask);
            bool canSeePlayer = (obstacles.Length == 0) && (distance < sightDistance) && (angle > -(fov / 2)) && (angle < fov / 2);
            bool infrontOfPlayer = (obstacles.Length == 0) && (distance < Vector3.Distance(attackPos.position, transform.position)) && (angle > -(fov / 4)) && (angle < fov / 4);

            state = machine.Run(animDone, canSeePlayer, player.dead, health < 2, infrontOfPlayer, distance, hit, anim.GetCurrentAnimatorClipInfo(0));

            switch (state)
            {
                case (int)EnemyStateMachine.State.Wait: Wait(); break;
                case (int)EnemyStateMachine.State.Patrol: Patrol(); break;
                case (int)EnemyStateMachine.State.Flee: Flee(); break;
                case (int)EnemyStateMachine.State.Chase: Chase(); break;
                case (int)EnemyStateMachine.State.Tired: Tired(); break;
                case (int)EnemyStateMachine.State.Attack: Attack(); break;
                case (int)EnemyStateMachine.State.Hit: Hit(); break;
            }
            if (hit)
            {
                hit = false;
                health--;
                if (health <= 0)
                    Destroy(this.gameObject);
            }
        }

        public void AnimDetector()
        {
            animDone = true;
        }

        void OnTriggerEnter(Collider col)
        {
            if (Util.GameState.paused)
                return;
            hit = true;
        }

        private void Wait()
        {
        }
        private void Patrol()
        {
            
            if (Vector3.Distance(wayPoints[currentNode].transform.position, transform.position) < 2)
                currentNode++;
            if (currentNode >= wayPoints.Length)
                currentNode = 0;
            RunWhiskerNav(wayPoints[currentNode].position, runSpeed / 2, backPedalSpeed / 2);
            
        }
        private void Flee()
        {
            RunWhiskerNav(Vector3.LerpUnclamped(player.transform.position, transform.position, 2f), runSpeed, backPedalSpeed);
        }
        private void Chase()
        {
            RunWhiskerNav(player.transform.position, runSpeed, backPedalSpeed);
        }
        private void Tired()
        {
        }
        private void Attack()
        {
            if (!doOnce)
            {
                doOnce = true;
                attackInstance = Instantiate(attack);
                attackInstance.transform.position = attackPos.position;
            }
        }
        private void Hit()
        {
        }

        private void RunWhiskerNav(Vector3 target, float runSpeed, float backPedalSpeed)
        {
            if (!backOff && backUpTimer > backUpDuration)
                transform.position += transform.forward * runSpeed;
            else
                transform.position += -transform.forward * backPedalSpeed;

            if (backUpTimer <= backUpDuration)
                backUpTimer++;

            Whisker();
            TargetTracking(target);
        }

        private void Whisker()
        {
            Vector3 leftWhiskerVector = -transform.right + transform.forward;
            Vector3 rightWhiskerVector = transform.right + transform.forward;

            RaycastHit[] emergencyWhiskerRight = Physics.RaycastAll(transform.position, rightWhiskerVector, emergencyWhiskerLength, navObjectMask);
            RaycastHit[] emergencyWhiskerLeft = Physics.RaycastAll(transform.position, leftWhiskerVector, emergencyWhiskerLength, navObjectMask);
            RaycastHit[] whiskerLeft = Physics.RaycastAll(transform.position, leftWhiskerVector, whiskerLength, navObjectMask);
            RaycastHit[] whiskerRight = Physics.RaycastAll(transform.position, rightWhiskerVector, whiskerLength, navObjectMask);

            if (emergencyWhiskerLeft.Length > 0 && emergencyWhiskerRight.Length > 0)
            {
                backOff = true;
                backUpTimer = 0;
            }
            else
                backOff = false;

            if (whiskerLeft.Length > 0)
                transform.Rotate(0, maxTurnSpeed, 0);
            else if (whiskerRight.Length > 0)
                transform.Rotate(0, -maxTurnSpeed, 0);

            Debug.DrawRay(transform.position, whiskerLength * leftWhiskerVector, Color.red);
            Debug.DrawRay(transform.position, emergencyWhiskerLength * rightWhiskerVector, Color.blue);
        }

        private void TargetTracking(Vector3 target)
        {
            Vector3 targetDirVec = target - transform.position;
            float targetDistance = Vector3.Distance(target, transform.position);
            RaycastHit[] obstacles = Physics.RaycastAll(transform.position, targetDirVec, targetDistance, navObjectMask);
            if (obstacles.Length == 0)
            {

                Transform transformCopyLeft = this.transform;
                Transform transformCopyRight = this.transform;

                transformCopyLeft.Rotate(0, -maxTurnSpeed, 0);
                Vector3 targetDirVecLeft = new Vector3(transformCopyLeft.forward.x, transformCopyLeft.forward.y, transformCopyLeft.forward.z);


                transformCopyRight.Rotate(0, 2 * maxTurnSpeed, 0);

                Vector3 targetDirVecRight = new Vector3(transformCopyRight.forward.x, transformCopyRight.forward.y, transformCopyRight.forward.z);

                transformCopyRight.Rotate(0, -maxTurnSpeed, 0);
                Debug.DrawRay(transform.position, targetDirVec, Color.black);
                //Debug.DrawRay(transform.position, targetDirVecLeft, Color.yellow,1f);
                //Debug.DrawRay(transform.position, targetDirVecRight, Color.green,1f);

                float angleLeft = Vector3.Angle(targetDirVecLeft, targetDirVec);
                float angleRight = Vector3.Angle(targetDirVecRight, targetDirVec);


                if (angleLeft < angleRight)
                {
                    if (angleLeft > 5)
                        transform.Rotate(0, -maxTurnSpeed, 0);
                }
                else
                {
                    if (angleRight > 5)
                        transform.Rotate(0, maxTurnSpeed, 0);
                }
            }
        }
    }
}
