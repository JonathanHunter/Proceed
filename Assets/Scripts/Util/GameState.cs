//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey

using UnityEngine;

namespace Assets.Scripts.Util
{
    class GameState : MonoBehaviour
    {
        /// <summary> All possible states for the game. </summary>
        public enum State { Playing, Paused, EndLevel, Gameover };

        /// <summary> The difficulty of the current level. </summary>
        public int difficulty = 0;

        /// <summary> The number of player deaths so far on this level. </summary>
        public int playerDeaths = 0;

        /// <summary> Number of Lives the Player has left. </summary>
        public int Lives = 0;

        /// <summary> The current state of the game. </summary>
        public static State state = State.Playing;
    }
}
