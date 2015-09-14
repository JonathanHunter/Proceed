using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.ProceduralGen
{
    class SequenceChecker
    {
        /// <summary> One index per room with each element pointing to an array of illegal follow up rooms denoted by their number, -1 means any room can follow. </summary>
        private static int[][] invalidFollowUp = new int[][] {
            new int[] { 3 },            // room 1
            new int[] { -1 },           // room 2
            new int[] { 2 } };          // room 3

        private static int TotalAttempts = 10000;

        /// <summary> Error checks a sequence of rooms and fixes any invalid series </summary>
        /// <param name="input"> The sequence of rooms. </param>
        /// <returns> An error checked sequence of rooms. </returns>
        public static int[] ErrorCheckSequence(int[] input, int seed)
        {
            Random.seed = seed;
            List<int> sequence = new List<int>();
            sequence.Add(input[0]);
            for (int i = 0; i < input.Length - 1; i++)
                sequence.Add(replaceNumber(i, input));
            return sequence.ToArray();
        }

        /// <summary> Replaces the next number in the sequence with a valid one</summary>
        /// <param name="index"> The currently correct number</param>
        /// <param name="arr"> The input array</param>
        /// <exception cref="UnableToCorrectSequenceException"> Thrown if the sequence can't be repaired </exception>
        /// <returns> The new number </returns>
        private static int replaceNumber(int index, int[] arr)
        {
            if (index >= arr.Length - 1)
                return -1;

            if (invalidFollowUp[arr[index]].Contains(arr[index + 1]))
            {
                int temp = 0;
                int attemps = 0;
                bool failed = false;
                while (!failed && invalidFollowUp[arr[index]].Contains(temp = Random.Range(0, invalidFollowUp.Length)))
                {
                    if (++attemps > TotalAttempts)
                        failed = true;
                }

                if (failed)
                    throw new UnableToCorrectSequenceException("Failed to find a good replacement for " + arr[index + 1] + " given " + invalidFollowUp[arr[index]]);
                return temp;
            }
            return arr[index + 1];
        }

    }
}
