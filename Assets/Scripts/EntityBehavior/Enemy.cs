using UnityEngine;

namespace Assets.Scripts.EntityBehavior
{
    class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private int startHealth;
        [SerializeField]
        private float aggresionRadius;
        [SerializeField]
        private float fleeDistance;
        [SerializeField]
        private float timeCanChase;
        [SerializeField]
        private float waitTime;
        [SerializeField]
        private float restTime;
        [SerializeField]
        private float fov;
        [SerializeField]
        private bool coward;
        [SerializeField]
        private bool fearless;

        private Player.PlayerController player;
        private bool doOnce, animDone, hit;
        private int state, prevState, health;
        private EnemyStateMachine machine;

        void Start()
        {
            player = FindObjectOfType<Player.PlayerController>();
            state = 0;
            prevState = 0;
            health = startHealth;
            doOnce = false;
            animDone = false;
            machine = new EnemyStateMachine(aggresionRadius, fleeDistance, timeCanChase, waitTime, restTime, coward, fearless);
        }

        void Update()
        {
            if (state != prevState)
            {
                doOnce = false;
                prevState = state;
                anim.SetInteger("state", state);
            }

            state = machine.Run(animDone, playerSpotted, player.dead, health < 2, infrontOfPlayer, Vector3.Distance(player.transform.position, transform.position), hit);

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
            hit = true;
        }

        private void Wait()
        {

        }
        private void Patrol()
        {

        }
        private void Flee()
        {

        }
        private void Chase()
        {

        }
        private void Tired()
        {

        }
        private void Attack()
        {

        }
        private void Hit()
        {

        }
    }
}
