using UnityEngine;

namespace Assets.Scripts.Platforms
{
    class CoinSpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject Coin;
        [SerializeField]
        private Transform[] spawnPoints;

        void Start()
        {
            if (spawnPoints.Length == 0)
                return;
            int point = Random.Range(0, spawnPoints.Length);
            GameObject temp = Instantiate(Coin);
            temp.transform.position = spawnPoints[point].position;
            temp.transform.parent = this.gameObject.transform;
            Util.GameState g = FindObjectOfType<Util.GameState>();
            if(g != null)
                g.currentObtainableCoins++;
        }
    }
}
