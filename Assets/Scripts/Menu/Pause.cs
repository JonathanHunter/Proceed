using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu
{
    class Pause : MonoBehaviour
    {
        [SerializeField]
        private Canvas menu;
        [SerializeField]
        private GameObject selected;

        private GameObject currentSelected;

        void Start()
        {
            menu.enabled = false;
            EventSystem.current.SetSelectedGameObject(selected);
        }

        void Update()
        {
            if(Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Pause))
            {
                Util.GameState.paused = !Util.GameState.paused;
                menu.enabled = Util.GameState.paused;
            }
            if(menu.enabled)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                    EventSystem.current.SetSelectedGameObject(selected);

                currentSelected = EventSystem.current.currentSelectedGameObject;

                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Up))
                    Navigator.Navigate(Util.CustomInput.UserInput.Up, currentSelected);
                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Down))
                    Navigator.Navigate(Util.CustomInput.UserInput.Down, currentSelected);
                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Accept))
                    Navigator.CallSubmit();
                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Cancel))
                    Resume();
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
