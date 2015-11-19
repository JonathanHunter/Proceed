//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    class Spin : MonoBehaviour
    {
        public float spinSpeed = 1f;
        public bool spinDirection = true;
        private int polarity = 1;

        void Start()
        {

        }

        void Update()
        {
            if (Util.GameState.paused)
                return;
            if (spinDirection)
            {
                polarity = 1;
            }
            else
            {
                polarity = -1;
            }

            transform.Rotate(0, spinSpeed * polarity * Time.deltaTime, 0);
        }
    }
}
