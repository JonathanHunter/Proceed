using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProceduralGen
{
    class SequenceChecker
    {
        /// <summary> One index per room with each element pointing to an array of illegal follow up rooms denoted by their number, -1 means any room can follow. </summary>
        private int[][] invalidFollowUp = new int[][] {
            new int[] { 3 },            // room 1
            new int[] { -1 },           // room 2
            new int[] { 2 } };          // room 3

        /// <summary> Error checks a sequence of rooms and fixes any invalid series </summary>
        /// <param name="input"> The sequence of rooms. </param>
        /// <returns> An error checked sequence of rooms. </returns>
        public static int[] ErrorCheckSequence(int[] input)
        {
            List<int> sequence = new List<int>();

            return sequence.ToArray();
        }


    }
}
