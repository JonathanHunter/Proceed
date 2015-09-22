using UnityEngine;
using System.Collections;

public class HorizontalTranslate : MonoBehaviour {
    public float speed, maxTranslate;

    float translated;

	// Use this for initialization
	void Start () {
        translated = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed, 0f, 0f);
        translated += Mathf.Abs(speed);

        if (translated >= maxTranslate) {
            speed *= -1;
            translated = 0.0f;
        }
    }
}
