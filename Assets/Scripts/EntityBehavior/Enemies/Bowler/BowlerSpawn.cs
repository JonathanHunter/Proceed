//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.EntityBehavior.Enemies.Bowler
{
    public class BowlerSpawn : MonoBehaviour
    {

        [SerializeField]
        private GameObject bowler;
        [SerializeField]
        private Transform[] spawnPoints;
        [SerializeField]
        private float spawnDelay;

        private int newSpawn;
        private int lastSpawn = -1;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(SpawnBowlers());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator SpawnBowlers()
        {
            while (true)
            {
                // make sure we don't spawn a bowler at the same place twice in a row
                newSpawn = Random.Range(0, spawnPoints.Length);
                while (newSpawn == lastSpawn)
                {
                    newSpawn = Random.Range(0, spawnPoints.Length);
                }
                lastSpawn = newSpawn;

                Instantiate(bowler, spawnPoints[newSpawn].transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
