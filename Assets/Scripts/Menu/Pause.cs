//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
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
            if (Util.GameState.state == Util.GameState.State.EndLevel || Util.GameState.state == Util.GameState.State.Gameover)
                return;
            if (Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Pause))
            {
                if (Util.GameState.state != Util.GameState.State.Paused)
                {
                    Util.GameState.state = Util.GameState.State.Paused;
                    EventSystem.current.SetSelectedGameObject(selected);
                }
                else
                    Util.GameState.state = Util.GameState.State.Playing;

                menu.enabled = Util.GameState.state == Util.GameState.State.Paused;
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
            Util.GameState.state = Util.GameState.State.Playing;
            menu.enabled = false;
        }

        public void Quit()
        {
            Util.GameState.state = Util.GameState.State.Playing;
            Application.LoadLevel("MainMenu");
        }
    }
}
