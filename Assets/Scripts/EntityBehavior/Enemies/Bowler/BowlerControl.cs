using UnityEngine;
using System.Collections;

public class BowlerControl : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform platform;

    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
	}

    void FixedUpdate()
    {
        rb.AddForce(platform.right * speed);
    }
}
