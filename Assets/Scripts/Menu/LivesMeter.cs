using UnityEngine;

namespace Assets.Scripts.Menu
{
    class LivesMeter : MonoBehaviour
    {
        public UnityEngine.UI.Text text;

        private Util.GameState gamestate;

        void Start()
        {
            gamestate = FindObjectOfType<Util.GameState>();
        }

        void Update()
        {
            text.text = "Lives: " + gamestate.lives;
        }
    }
}
