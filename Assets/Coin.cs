using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public float xRot = 0;
	public float yRot = 0;
	public float zRot = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(xRot, yRot, zRot);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);
		}
	}
}
