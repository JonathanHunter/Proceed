using UnityEngine;
using System.Collections;

public class TestSpin : MonoBehaviour {

	public float spinSpeed = 1f;
	public bool spinDirection = true;
	private int polarity = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(spinDirection){
			polarity = 1;
		}
		else{
			polarity = -1;
		}

		transform.Rotate(0, spinSpeed * polarity , 0);
	}
}
