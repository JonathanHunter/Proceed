using UnityEngine;
using System.Collections;

public class BowlerSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject bowler;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private float spawnDelay;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SpawnBowlers());
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    IEnumerator SpawnBowlers()
    {
        while (true)
        {
            Instantiate(bowler, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
