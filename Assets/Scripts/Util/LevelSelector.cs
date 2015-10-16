//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Util
{
    class LevelSelector : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                Application.LoadLevel(1);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                Application.LoadLevel(2);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                Application.LoadLevel(3);
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                Application.LoadLevel(4);
            }
        }
    }
}
