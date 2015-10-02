﻿using UnityEngine;
using Assets.Scripts.Util;

namespace Assets.Scripts.ProceduralGen
{
    public class LevelGenerator : MonoBehaviour
    {
        /// <summary> The set of blocks used to generate levels. </summary>
        [SerializeField]
        private BlockSet blockSet;

        /// <summary> Reference t the current GameState. </summary>
        private GameState gameState;

        /// <summary> Bool as to whether or not a level has been instanctiated. </summary>
        private bool levelInProgress;

        /// <summary> Top level reference to the level objects. </summary>
        private GameObject levelRef;

        void Start()
        {
            gameState = FindObjectOfType<GameState>();
            if (gameState == null)
            {
                Debug.LogError("GameState Object not found in scene!");
                Destroy(this.gameObject);
            }
            levelInProgress = false;
            StartLevel();
        }

        /// <summary> Starts a new Level. </summary>
        public void StartLevel()
        {
            if (levelInProgress)
                EndLevel();
            DetermineDifficulty();
            int[] level = GenerateLevel(gameState.difficulty, 0, gameState.difficulty > blockSet.blocks.Length ? blockSet.blocks.Length : gameState.difficulty, Random.Range(0,50000));
            InstantiateGameObjects(level);
            levelInProgress = true;
        }

        /// <summary> Cleans up all the objects in a scene related to the level. </summary>
        public void EndLevel()
        {
            if (!levelInProgress)
                return;
            Destroy(levelRef);
            //TODO: Clean up any spawned objects, bullets, enemies, etc.
            levelInProgress = false;
        }
        
        /// <summary> Sets the difficulty for the next level based on  player performance. </summary>
        private void DetermineDifficulty()
        {
            if (gameState.difficulty == 0) //game start
                gameState.difficulty = 3;
            else
            {
                if (gameState.playerDeaths == 0)
                    gameState.difficulty += 2;
                else if (gameState.playerDeaths < 5)
                    gameState.difficulty++;
                else if (gameState.playerDeaths < 10)
                    gameState.difficulty--;
                else
                    gameState.difficulty -= 2;
            }
            gameState.playerDeaths = 0;
        }

        /// <summary> Generates an error checked level sequence. </summary>
        /// <param name="length"> The length of the level to generate., </param>
        /// <param name="rangeMin"> The minimum number to insert in the sequence. </param>
        /// <param name="rangeMax"> The maximum number to insert in the sequence. </param>
        /// <param name="seed"> The seed for the RNG. </param>
        /// <exception cref="UnableToCorrectSequenceException"> Thrown if the sequence can't be repaired. </exception>
        /// <returns> A level sequence. </returns>
        private int[] GenerateLevel(int length, int rangeMin, int rangeMax, int seed)
        {
            Random.seed = seed;
            int[] arr = new int[length];
            for (int i = 0; i < length; i++)
            {
                arr[i] = Random.Range(rangeMin, rangeMax);
            }
            return SequenceChecker.ErrorCheckSequence(arr, seed, rangeMin, rangeMax);
        }

        /// <summary> Spawns the sequence of blocks. </summary>
        /// <param name="sequence"> The sequence of blocks to instantiate. </param>
        private void InstantiateGameObjects(int[] sequence)
        {
            levelRef = new GameObject("map");
            Block curBlock = null, lastBlock = null;
            lastBlock = Instantiate(blockSet.startBlock.gameObject).GetComponent<Block>();
            lastBlock.transform.parent = levelRef.transform;
            lastBlock.transform.localPosition = new Vector3(0, 0, 0);
            for (int i = 0; i < sequence.Length; i++)
            {
                if (sequence[i] == -1)
                    curBlock = Instantiate(blockSet.ErrorBlock.gameObject).GetComponent<Block>();
                else
                    curBlock = Instantiate(blockSet.blocks[sequence[i]].gameObject).GetComponent<Block>();
                curBlock.transform.parent = levelRef.transform;
                curBlock.transform.localPosition = new Vector3(lastBlock.endBlock.position.x, 0, 0);
                lastBlock = curBlock;
            }
            curBlock = Instantiate(blockSet.endBlock.gameObject).GetComponent<Block>();
            curBlock.transform.parent = levelRef.transform;
            curBlock.transform.localPosition = new Vector3(lastBlock.endBlock.position.x, 0, 0);
        }
    }
}