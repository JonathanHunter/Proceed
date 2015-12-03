//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu
{
    class GameOver : MonoBehaviour
    {
        [SerializeField]
        private Canvas menu;
        [SerializeField]
        private GameObject selected;
        [SerializeField]
        private UnityEngine.UI.Text total;
        [SerializeField]
        private UnityEngine.UI.Text levelsCleared;
        [SerializeField]
        private UnityEngine.UI.Text coins;
        [SerializeField]
        private UnityEngine.UI.Text totalHigh;
        [SerializeField]
        private UnityEngine.UI.Text levelsClearedHigh;
        [SerializeField]
        private UnityEngine.UI.Text coinsHigh;
        [SerializeField]
        private GameObject newHigh;
        [SerializeField]
        private GameObject oldHigh;


        private GameObject currentSelected;
        private float startTime, timeTotal;
        private bool doOnce = false;
        private bool set = false;
        private int numOfLevels, coinNum, coinTNum;
        private string hash = "Proceed";

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
                    Util.GameState.state = Util.GameState.State.Gameover;
                    FindObjectOfType<Player.PlayerController>().transform.parent = null;
                    set = false;
                }

                if (!doOnce)
                {
                    int oldLevel = 0;
                    float oldTime = 999f;
                    int oldCoins = 0;
                    int oldCoinsT = 999;
                    if(PlayerPrefs.HasKey(hash + "Level"))
                    {
                        oldLevel = PlayerPrefs.GetInt(hash + "Level");
                        oldTime = PlayerPrefs.GetFloat(hash + "Time");
                        oldCoins = PlayerPrefs.GetInt(hash + "Coins");
                        oldCoinsT = PlayerPrefs.GetInt(hash + "CoinsT");
                    }
                    Util.GameState state = FindObjectOfType<Util.GameState>();
                    timeTotal = state.time;
                    numOfLevels = state.numOfLevels;
                    coinNum = state.totalCoins + state.currentCoins;
                    coinTNum = state.totalObtainableCoins + state.currentObtainableCoins;
                    total.text = timeTotal.ToString("N2") + " sec";
                    levelsCleared.text = numOfLevels + "";
                    coins.text = coinNum + " / " + coinTNum;
                    if (numOfLevels > oldLevel || (numOfLevels == oldLevel && ((coinNum / coinTNum) > (oldCoins / oldCoinsT) || timeTotal < oldTime)))
                    {
                        newHigh.SetActive(true);
                        oldHigh.SetActive(false);
                        PlayerPrefs.SetInt(hash + "Level", numOfLevels);
                        PlayerPrefs.SetFloat(hash + "Time", timeTotal);
                        PlayerPrefs.SetInt(hash + "Level", numOfLevels);
                        PlayerPrefs.SetInt(hash + "Level", numOfLevels);
                    }
                    else
                    {
                        newHigh.SetActive(false);
                        oldHigh.SetActive(true);
                        totalHigh.text = oldTime.ToString("N2") + " sec";
                        levelsClearedHigh.text = oldLevel + "";
                        coinsHigh.text = oldCoins + " / " + oldCoinsT;
                    }
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

        public void Retry()
        {
            Util.GameState.state = Util.GameState.State.Playing;
            menu.enabled = false;
            FindObjectOfType<Util.GameState>().Reset();
            FindObjectOfType<Player.PlayerController>().Respawn();
            Application.LoadLevel("ProcedualLevel");
        }

        public void Quit()
        {
            FindObjectOfType<Util.GameState>().Reset();
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
