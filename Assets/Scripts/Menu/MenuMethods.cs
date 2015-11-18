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
        private GameObject creditsParent;
        [SerializeField]
        private GameObject creditsSelected;

        private GameObject currentSelected;
        private bool inCredits;

        void Start()
        {
            inCredits = false;
            EventSystem.current.SetSelectedGameObject(mainSelected); 
        }

        void Update()
        {
            currentSelected = EventSystem.current.currentSelectedGameObject;
            if (Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Up))
                Navigator.Navigate(Util.CustomInput.UserInput.Up, currentSelected);
            if (Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Down))
                Navigator.Navigate(Util.CustomInput.UserInput.Down, currentSelected);
            if (Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Accept))
                Navigator.CallSubmit();
            if (inCredits && Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Cancel))
                GoToMain();
        }

        public void Play()
        {
            Application.LoadLevel("ProcedualLevel");
        }

        public void GoToMain()
        {
            inCredits = false;
            mainParent.SetActive(true);
            creditsParent.SetActive(false);
            EventSystem.current.SetSelectedGameObject(mainSelected);
        }

        public void GoToCredits()
        {
            inCredits = true;
            mainParent.SetActive(false);
            creditsParent.SetActive(true);
            EventSystem.current.SetSelectedGameObject(creditsSelected);
        }
    }
}
