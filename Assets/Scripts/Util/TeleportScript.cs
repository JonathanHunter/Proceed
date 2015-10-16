//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Util
{
    class TeleportScript : MonoBehaviour
    {
        public Vector3 pos;
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                col.transform.parent = null;
                col.transform.position = pos;
            }
        }
    }
}
