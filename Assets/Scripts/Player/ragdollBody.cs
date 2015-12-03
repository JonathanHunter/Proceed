//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ragdollBody : MonoBehaviour
    {

        public float destructionTimer = 3f;
        public Transform HipN;
		public bool shouldDisappear = true;

        // Use this for initialization
        void Start()
        {

        }

		// Update is called once per frame
		void Update()
		{
			if (shouldDisappear || transform.position.y < -20)
			{
				if (destructionTimer > 0)
				{
					destructionTimer -= Time.deltaTime;
				}
				else
				{
					Destroy(this.gameObject);
				}
			}
        }
    }
}
