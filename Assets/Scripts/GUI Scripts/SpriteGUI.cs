using UnityEngine;
using System.Collections;

public class SpriteGUI : MonoBehaviour {
	void Start () {
		GetComponent<SpriteRenderer> ().color = new Color(1, 1, 1, 0);
	}

	void Update () {
		GetComponent<SpriteRenderer> ().color = new Color(1, 1, 1, 1);
	}
}
