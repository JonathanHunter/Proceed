using UnityEngine;
using System.Collections;

public class PlayerMarkerDebug : MonoBehaviour {

    PlayerMarkerParent parent;

	// Use this for initialization
	void Awake () 
    {
        parent = FindObjectOfType<PlayerMarkerParent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("enemy"))
        {
            parent.SwapMarkers(this);
        }
    }
}
