//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey

using UnityEngine;

namespace Assets.Scripts.Util
{
    class GameState : MonoBehaviour
    {
        /// <summary> The difficulty of the current level. </summary>
        public int difficulty = 0;

        /// <summary> The number of player deaths so far on this level. </summary>
        public int playerDeaths = 0;

        /// <summary> Used to stop all objects when the pause menu comes up </summary>
        public static bool paused = false;
    }
}
