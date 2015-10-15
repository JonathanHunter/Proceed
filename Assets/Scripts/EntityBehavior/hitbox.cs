using UnityEngine;
using System.Collections;

public class hitbox : MonoBehaviour {

	public float destructionTimer = 1f;
	public float knockback = 200f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(destructionTimer >0){
			destructionTimer -= Time.deltaTime;
		}
		else{
			Destroy(this.gameObject);
		}
	}
}
