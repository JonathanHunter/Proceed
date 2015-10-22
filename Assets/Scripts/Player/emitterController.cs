//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class emitterController : MonoBehaviour
    {

        public ParticleSystem stepEmitter;

        // Use this for initialization
        void Start()
        {
            stepEmitter.enableEmission = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void emitStep()
        {
            stepEmitter.Emit(1);
        }
    }
}
