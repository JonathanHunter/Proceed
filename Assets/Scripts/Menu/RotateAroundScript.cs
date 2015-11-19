using UnityEngine;

namespace Assets.Scripts.Menu
{
    class RotateAroundScript : MonoBehaviour
    {
        public GameObject thing;
        public float angle;

        void Update()
        {
            this.transform.LookAt(thing.transform);
            this.transform.RotateAround(thing.transform.position, new Vector3(0, 1, 0), angle * Mathf.Deg2Rad * Time.deltaTime);
        }
    }
}
