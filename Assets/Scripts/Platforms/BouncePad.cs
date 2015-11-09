//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    public class BouncePad : MonoBehaviour
    {
        public float bounceForce;
        public AudioClip boing;
        public AudioSource soundPlayer;

        void Start()
        {
            //Physics.IgnoreCollision(this.GetComponent<Collider>(), pad);
            //Physics.IgnoreCollision(this.GetComponent<Collider>(), padBase);
        }

        void OnTriggerEnter(Collider col)
        {
            if (Util.GameState.paused)
                return;
            Rigidbody rgbdy = col.gameObject.GetComponent<Rigidbody>();
            //if (rgbdy == null)
            //{
            //    Transform parent = col.gameObject.transform.parent;
            //    while (parent != null && rgbdy == null)
            //    {
            //        rgbdy = parent.gameObject.GetComponent<Rigidbody>();
            //        parent = parent.parent;
            //    }
            //}

            if (rgbdy != null)
            {
                rgbdy.AddForce(this.transform.up * bounceForce, ForceMode.Impulse);
                soundPlayer.PlayOneShot(boing);
            }
        }
    }
}
