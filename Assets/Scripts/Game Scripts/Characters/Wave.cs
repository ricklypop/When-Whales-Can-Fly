using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
    public float maxHeight;
    public float currentHeight;
    private bool direction;
	private float extra;
	private Vector3 basePosition;

	void Start(){
	}

	void Update () {
        if (currentHeight >= maxHeight)
        {
			extra = currentHeight - maxHeight;
            direction = !direction;
            currentHeight = 0;
        }

        currentHeight += transform.up.normalized.y / 1;

        if (direction) {
			transform.position += transform.up / 2 * Time.deltaTime;
			transform.position += new Vector3(0, extra, 0);
			extra = 0;
		}
        if (!direction) {
			transform.position -= transform.up / 2 * Time.deltaTime;
			transform.position -= new Vector3(0, extra, 0);
			extra = 0;
		}
	}
}
