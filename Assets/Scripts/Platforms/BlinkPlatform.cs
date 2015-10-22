using UnityEngine;
using System.Collections;

public class BlinkPlatform : MonoBehaviour {
    public GameObject[] oddCubes, evenCubes;
    public float timeLimit;

    private float timer;
    private bool oddActive;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        timer = timeLimit;

        foreach (GameObject cube in evenCubes) {
            cube.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        anim.ResetTrigger("AboutToDisappear");

        if (timer <= 0f) {
            if (oddActive) {
                foreach (GameObject cube in oddCubes) {
                    cube.SetActive(false);
                }
                foreach (GameObject cube in evenCubes) {
                    cube.SetActive(true);
                }
                timer = timeLimit;
                oddActive = false;
            } else {
                foreach (GameObject cube in oddCubes) {
                    cube.SetActive(true);
                }
                foreach (GameObject cube in evenCubes) {
                    cube.SetActive(false);
                }
                timer = timeLimit;
                oddActive = true;
            }
        } else if (timer <= 1f) {
            anim.SetTrigger("AboutToDisappear");
        }
	}

}
