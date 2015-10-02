using UnityEngine;

namespace Assets.Scripts.ProceduralGen
{
    class Block : MonoBehaviour 
    {
        /// <summary> The length of this block. </summary>
        public float length;

        /// <summary> The first block of this obstacle. </summary>
        public Transform startBlock;

        /// <summary> The last block of this obstacle. </summary>
        public Transform endBlock;
    }
}
