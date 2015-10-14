﻿using UnityEngine;

namespace Assets.Scripts.Util
{
    class CameraTracking : MonoBehaviour
    {
        [SerializeField]
        private Transform player;
		public float speed = .5f;

        void Start()
        {
        }

        void Update()
        {
            float speed = Time.deltaTime * ((Mathf.Ceil(Mathf.Abs(this.transform.position.x - player.position.x))) + 10);
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.position.x + 6, player.transform.position.y + 4, player.transform.position.z), speed);
			//transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + 6, player.transform.position.y + 4, player.transform.position.z), speed);
        }
    }
}
