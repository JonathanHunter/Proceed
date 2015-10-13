using UnityEngine;
using System.Collections;

public class ragdollBody : MonoBehaviour {

	public float destructionTimer = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(destructionTimer >0){
			destructionTimer -= Time.DetlatTime;
		}
		else{
			Destroy(this);
		}
	}
}
