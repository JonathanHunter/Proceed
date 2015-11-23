using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu
{
    class MenuMethods : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainParent;
        [SerializeField]
        private GameObject mainSelected;
        [SerializeField]
        private GameObject instructionsParent;
        [SerializeField]
        private GameObject instructionsSelected;
        [SerializeField]
        private GameObject creditsParent;
        [SerializeField]
        private GameObject creditsSelected;

        private GameObject currentSelected;
        private bool inCredits;
        private bool inInstructions;

        void Start()
        {
            inCredits = false;
            inInstructions = false;
            EventSystem.current.SetSelectedGameObject(mainSelected); 
        }

        void Update()
        {
            if (!inInstructions && ! inCredits)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    if(inCredits)
                        EventSystem.current.SetSelectedGameObject(creditsSelected);
                    else
                        EventSystem.current.SetSelectedGameObject(mainSelected);
                }

                currentSelected = EventSystem.current.currentSelectedGameObject;

                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Up))
                    Navigator.Navigate(Util.CustomInput.UserInput.Up, currentSelected);
                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Down))
                    Navigator.Navigate(Util.CustomInput.UserInput.Down, currentSelected);
                if (Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Accept))
                    Navigator.CallSubmit();
                if (inCredits && Util.CustomInput.BoolFreshPressDeleteOnRead(Util.CustomInput.UserInput.Cancel))
                    GoToMain();
            }
        }

        public void Play()
        {
            Util.GameState.state = Util.GameState.State.Playing;
            Application.LoadLevel("ProcedualLevel");
        }

        public void GoToMain()
        {
            inCredits = false;
            inInstructions = false;
            mainParent.SetActive(true);
            instructionsParent.SetActive(false);
            creditsParent.SetActive(false);
            EventSystem.current.SetSelectedGameObject(mainSelected);
        }

        public void GoToCredits()
        {
            inCredits = true;
            inInstructions = false;
            mainParent.SetActive(false);
            instructionsParent.SetActive(false);
            creditsParent.SetActive(true);
            EventSystem.current.SetSelectedGameObject(creditsSelected);
        }

        public void GoToInstructions()
        {
            inCredits = false;
            inInstructions = true;
            mainParent.SetActive(false);
            instructionsParent.SetActive(true);
            creditsParent.SetActive(false);
            EventSystem.current.SetSelectedGameObject(instructionsSelected);

        }
    }
}
