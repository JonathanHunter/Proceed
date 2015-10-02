using UnityEngine;

namespace Assets.Scripts.ProceduralGen
{
    class BlockSet : MonoBehaviour
    {
        /// <summary> The error control block. </summary>
        public Block ErrorBlock;

        /// <summary> Prefabs for a level begin. </summary>
        public Block startBlock;

        /// <summary> Prefabs for a level end. </summary>
        public Block endBlock;

        /// <summary> Prefabs for level segments. </summary>
        public Block[] blocks;
    }
}
