﻿//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey

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
        public int lives = 10;

        /// <summary> Number of levels the player has cleared. </summary>
        public int numOfLevels = 0;

        /// <summary> The current state of the game. </summary>
        public static State state = State.Playing;

        /// <summary> Current total playtime. </summary>
        public float time = 0f;

        /// <summary> The number of coins the player has collected this level. </summary>
        public int currentCoins;

        /// <summary> The total number of coins the player can collect this level. </summary>
        public int currentObtainableCoins;

        /// <summary> The total number of coins the player has actually collected. </summary>
        public int totalCoins;
        
        /// <summary> The total number of coins the player could have collected. </summary>
        public int totalObtainableCoins;
        
        void Update()
        {
            if (state == State.Playing)
                time += Time.deltaTime;
            if (lives == 0)
            {
                FindObjectOfType<Menu.GameOver>().Activate();
                lives = -1;
            }
        }

        /// <summary> Reset the game state to default. </summary>
        public void Reset()
        {
            difficulty = 0;
            playerDeaths = 0;
            lives = 10;
            numOfLevels = 0;
            state = State.Playing;
            time = 0f;
        }
    }
}
