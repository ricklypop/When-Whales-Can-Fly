using UnityEngine;
using System.Collections;

public class CustomizeSprite : MonoBehaviour {
    public float minSize;
    public float maxSize;
    public float minA;
    public float maxA;

	void Start () {
	    transform.localScale = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), 1);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Random.Range(minA, maxA));
	}
}
