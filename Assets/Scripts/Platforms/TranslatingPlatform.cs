using UnityEngine;

namespace Assets.Scripts.Platforms
{
    class TranslatingPlatform : MonoBehaviour
    {
        /// <summary> The first point to oscillate  between.</summary>
        [SerializeField]
        private Transform pointA;
        /// <summary> The second point to oscillate  between.</summary>
        [SerializeField]
        private Transform pointB;
        /// <summary> The speed to oscillate  at.</summary>
        [SerializeField]
        private float speed = 10f;

        /// <summary> The direction to travel in.</summary>
        private bool direction;
        /// <summary> The objects current location between the two points.</summary>
        private float currentPoint;

        void Start()
        {
            direction = false;
            currentPoint = 0f;
        }

        void Update()
        {
            if (direction)
                currentPoint += Time.deltaTime * speed;
            else
                currentPoint -= Time.deltaTime * speed;
            if (currentPoint > 1f)
            {
                direction = !direction;
                currentPoint = 1f;
            }
            if (currentPoint < 0f)
            {
                direction = !direction;
                currentPoint = 0f;
            }
            transform.position = Vector3.Lerp(pointA.position, pointB.position, currentPoint);
        }
    }
}
