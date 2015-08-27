using UnityEngine;
using System.Collections;

public class SlowOppisiteCloudDrift : MonoBehaviour {
	void Update () {
        transform.position += (transform.right / 3) * Time.deltaTime;
	}
}
