using UnityEngine;
using System.Collections;

public class emitterController : MonoBehaviour {

	public ParticleSystem stepEmitter;

	// Use this for initialization
	void Start () {
		stepEmitter.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void emitStep(){
        Debug.Log("A");
		stepEmitter.Emit (1);
	}
}
