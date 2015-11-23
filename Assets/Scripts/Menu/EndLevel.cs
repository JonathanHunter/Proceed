using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu
{
    class EndLevel : MonoBehaviour
    {
        [SerializeField]
        private Canvas menu;
        [SerializeField]
        private GameObject selected;
        [SerializeField]
        private UnityEngine.UI.Text timeThisLevel;
        [SerializeField]
        private UnityEngine.UI.Text total;
        [SerializeField]
        private UnityEngine.UI.Text livesLeft;

        private GameObject currentSelected;
        private float startTime, time, timeTotal;
        private bool doOnce = false;
        private bool set = false;
        private int lives;

        void Start()
        {
            startTime = FindObjectOfType<Util.GameState>().time;
            menu.enabled = false;
            EventSystem.current.SetSelectedGameObject(selected);
        }

        void Update()
        {
            if (menu.enabled)
            {
                if (set)
                {
                    Util.GameState.state = Util.GameState.State.EndLevel;
                    FindObjectOfType<Player.PlayerController>().transform.parent = null;
                    set = false;
                }

                if (!doOnce)
                {
                    timeTotal = FindObjectOfType<Util.GameState>().time;
                    time = timeTotal - startTime;
                    lives = FindObjectOfType<Util.GameState>().lives;
                    timeThisLevel.text = time.ToString("N2") + " sec";
                    total.text = timeTotal.ToString("N2") + " sec";
                    livesLeft.text = lives + "";
                    doOnce = true;
                }

                if (EventSystem.current.currentSelectedGameObject == null)
                    EventSystem.current.SetSelectedGameObject(selected);

                currentSelected = EventSystem.current.currentSelectedGameObject;

                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Left))
                    Navigator.Navigate(Util.CustomInput.UserInput.Left, currentSelected);
                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Right))
                    Navigator.Navigate(Util.CustomInput.UserInput.Right, currentSelected);
                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Accept))
                    Navigator.CallSubmit();
            }
        }

        public void Next()
        {
            Util.GameState.state = Util.GameState.State.Playing;
            menu.enabled = false;
            FindObjectOfType<Player.PlayerController>().Respawn();
            ProceduralGen.LevelGenerator level = FindObjectOfType<ProceduralGen.LevelGenerator>();
            level.EndLevel();
            level.StartLevel();
        }

        public void Quit()
        {
            Util.GameState.state = Util.GameState.State.Playing;
            FindObjectOfType<Player.PlayerController>().Respawn();
            Application.LoadLevel("MainMenu");
        }

        public void Activate()
        {
            set = true;
            menu.enabled = true;
            EventSystem.current.SetSelectedGameObject(selected);
        }
    }
}
