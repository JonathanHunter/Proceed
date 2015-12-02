//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.ProceduralGen
{
    class SequenceChecker
    {
        /// <summary> One index per room with each element pointing to an array of illegal follow up rooms denoted by their number, -1 means any room can follow. </summary>
        private static int[][] invalidFollowUp = new int[][] {
            new int[] { -1 },               // room 0   (Thin Platform)
            new int[] { 1, 3, 9, 10 },      // room 1   (HBlock)
            new int[] { 2, 5 },             // room 2   (Horizonal Translate)
            new int[] { 1, 3, 9, 10 },      // room 3   (AI platform open)
            new int[] { -1 },               // room 4   (Sandbox)
            new int[] { 2, 5 },             // room 5   (Vertical Translate platform)
            new int[] { 6 },                // room 6   (Cannon Spinning 1)
            new int[] { -1 },               // room 7   (Spin)
            new int[] { -1 },               // room 8   (trap)
            new int[] { 1, 3, 9, 10 },      // room 9   (AI Obstacle Platform)
            new int[] { 1, 3, 9, 10 },      // room 10  (AI Narrow Platform)
            new int[] { -1 },               // room 11  (Ice spin)
            new int[] { -1 },               // room 12  (Bounce Pads)
            new int[] { 13 },               // room 13  (Cannon Spinning 2)
            new int[] { 14, 16 },           // room 14  (Double spin 1)
            new int[] { -1 },               // room 15  (Tall platform)
            new int[] { 14, 16 } };         // room 16  (Double spin 2)

    private static int TotalAttempts = 10000;
          
        /// <summary> Error checks a sequence of rooms and fixes any invalid series. </summary>
        /// <param name="input"> The sequence of rooms. </param>
        /// <exception cref="UnableToCorrectSequenceException"> Thrown if the sequence can't be repaired. </exception>
        /// <returns> An error checked sequence of rooms. </returns>
        public static int[] ErrorCheckSequence(int[] input, int seed, int rangeMin, int rangeMax)
        {
            Random.seed = seed;
            List<int> sequence = new List<int>();
            sequence.Add(input[0]);
            for (int i = 0; i < input.Length - 1; i++)
                sequence.Add(ReplaceNumber(i, input, rangeMin, rangeMax));
            return sequence.ToArray();
        }

        /// <summary> Replaces the next number in the sequence with a valid one. </summary>
        /// <param name="index"> The currently correct number. </param>
        /// <param name="arr"> The input array. </param>
        /// <exception cref="UnableToCorrectSequenceException"> Thrown if the sequence can't be repaired. </exception>
        /// <returns> The new number. </returns>
        private static int ReplaceNumber(int index, int[] arr, int rangeMin, int rangeMax)
        {
            if (index >= arr.Length - 1)
                return -1;

            if (invalidFollowUp[arr[index]].Contains(arr[index + 1]))
            {
                int temp = 0;
                int attemps = 0;
                bool failed = false;
                while (!failed && invalidFollowUp[arr[index]].Contains(temp = Random.Range(rangeMin, rangeMax)))
                {
                    if (++attemps > TotalAttempts)
                        failed = true;
                }

                if (failed)
                    return -1;
                return temp;
            }
            return arr[index + 1];
        }

    }
}
