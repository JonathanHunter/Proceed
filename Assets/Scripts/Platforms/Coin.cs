using UnityEngine;

namespace Assets.Scripts.Platforms
{
    public class Coin : MonoBehaviour
    {
        private bool dead;

        void Start()
        {
            dead = false;
        }

        void Update()
        {
            if (dead)
            {
                FindObjectOfType<Util.GameState>().currentCoins++;
                Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
                dead = true;
        }
    }
}
