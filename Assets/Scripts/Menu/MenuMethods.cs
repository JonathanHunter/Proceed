using UnityEngine;

namespace Assets.Scripts.Menu
{
    class MenuMethods : MonoBehaviour
    {
        public void Play()
        {
            Application.LoadLevel("ProcedualLevel");
        }
    }
}
