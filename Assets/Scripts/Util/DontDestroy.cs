//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Util
{
    class DontDestroy : MonoBehaviour
    {
        private static GameObject instance;
        void Start()
        {
            if (instance == null)
            {
                instance = this.gameObject;
                DontDestroyOnLoad(this.gameObject);
            }
            else if(this.gameObject != instance)
                Destroy(this.gameObject);
        }
    }
}
