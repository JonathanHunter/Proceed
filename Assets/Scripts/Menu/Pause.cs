using UnityEngine;

namespace Assets.Scripts.Menu
{
    class Pause : MonoBehaviour
    {
        [SerializeField]
        private Canvas menu;

        void Start()
        {
            menu.enabled = false;
        }

        void Update()
        {
            if(Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Pause))
            {
                Util.GameState.paused = !Util.GameState.paused;
                menu.enabled = Util.GameState.paused;
            }
        }

        public void Resume()
        {
            Util.GameState.paused = false;
            menu.enabled = false;
        }

        public void Quit()
        {
            Util.GameState.paused = false;
            Application.LoadLevel("MainMenu");
        }
    }
}
