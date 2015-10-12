using UnityEngine;
using Assets.Scripts.Util;

public class OceanTracking : MonoBehaviour {

    Vector3 startingPoint;

	// Use this for initialization
	void Start () {
        this.transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(transform.position.y < 40f)
            this.transform.position = new Vector3(startingPoint.x, transform.position.y + 0.05f, startingPoint.z); //Player Position + rising y value;
	}
}
