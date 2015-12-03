//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Menu
{
    class Coins : MonoBehaviour
    {
        public UnityEngine.UI.Text text;

        private Util.GameState gamestate;

        void Start()
        {
            gamestate = FindObjectOfType<Util.GameState>();
        }

        void Update()
        {
            text.text = "Coins: " + gamestate.currentCoins + " / " + gamestate.currentObtainableCoins;
        }
    }
}
