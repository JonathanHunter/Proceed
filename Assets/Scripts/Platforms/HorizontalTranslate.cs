using UnityEngine;
using System.Collections;

public class HorizontalTranslate : MonoBehaviour {
    /// <summary> speed at which the platform moves </summary>
    public float speed;

    /// <summary> maximum distance the platform will move </summary>
    public float maxTranslate;

    /// <summary> keeps track of how far the platform has moved </summary>
    float translated;

	// Use this for initialization
	void Start () {
        translated = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed, 0f, 0f);
        translated += Mathf.Abs(speed);

        // reverse direction
        if (translated >= maxTranslate) {
            speed *= -1;
            translated = 0.0f;
        }
    }
}
