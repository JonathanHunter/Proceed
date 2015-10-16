//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using Assets.Scripts.EntityBehavior.Enemies.Bowler;

namespace Assets.Scripts.EntityBehavior.Enemies.Cannon
{
    public class cannonEnemy : MonoBehaviour
    {

        //Following Variables
        public GameObject player;
        public GameObject axis;

        //Firing variables
        float timerReset = 0;
        public float timer = 3f;
        public bool timerIsActive = true;
        public BowlerControl ammo;
        public float ammoLifespan = 3f;
        public Transform firingPoint;
        public float firingPower = 2f;



        // Use this for initialization
        void Start()
        {
            //This is so we can remember the intial timer value and reset it to it.
            timerReset = timer;
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            //DIE IF WE FALL
            if (this.transform.position.y <= -20)
            {
                Destroy(gameObject);
            }
            //IF WE SHOULD BE FIRING
            if (timerIsActive)
            {
                //COUNTDOWN TO FIRE
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    //THEN FIRE
                    timer = timerReset;
                    BowlerControl tempBall = GameObject.Instantiate<BowlerControl>(ammo);
                    tempBall.transform.position = firingPoint.position;
                    tempBall.GetComponent<Rigidbody>().AddForce(axis.transform.forward * firingPower);
                    tempBall.timerIsActive = true;
                    tempBall.timer = ammoLifespan;

                }
            }

            this.gameObject.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            axis.transform.LookAt(player.transform.position);
        }

        void OnTriggerEnter(Collider other)
        {

            //WE GET HIT
            if (other.tag == "Hitbox")
            {
                GetComponent<Rigidbody>().AddForceAtPosition(this.transform.forward * -1 * other.GetComponent<hitbox>().knockback, other.transform.position);
                Destroy(other);
            }
        }
    }
}
