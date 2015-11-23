//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.EntityBehavior.Enemies.Bowler
{
    public class BowlerControl : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private Transform platform;

        private Rigidbody rb;

        public float timer = 3f;
        public bool timerIsActive = false;

        private bool paused = false;
        private float animSpeed = 0;
        private Vector3 vel = new Vector3();

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Util.GameState.state == Util.GameState.State.Playing)
            {
                if (paused)
                {
                    paused = false;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().velocity = vel;
                }
                if (this.transform.position.y <= -20)
                {
                    Destroy(gameObject);
                }

                if (timerIsActive)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                if (!paused)
                {
                    GetComponent<Rigidbody>().useGravity = false;
                    vel = GetComponent<Rigidbody>().velocity;
                    GetComponent<Rigidbody>().velocity = new Vector3();
                    paused = true;
                }
            }
        }

        void FixedUpdate()
        {
            //rb.AddForce(platform.right * speed);
        }

        void OnCollisionEnter(Collision other)
        {
            // disabled for now - bowlers are tagged as enemies so death can be handled by PlayerController
            /*  if (other.gameObject.tag.Equals("Player"))
             {
                 Assets.Scripts.Player.PlayerController pc = other.rigidbody.GetComponentInParent<Assets.Scripts.Player.PlayerController>();
                 pc.die();
             } */
        }
    }
}
