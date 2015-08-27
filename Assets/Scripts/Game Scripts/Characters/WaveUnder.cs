using UnityEngine;
using System.Collections;

public class WaveUnder : MonoBehaviour
{
	public Transform topWave;
	private Vector3 lastPos;
	void Update ()
	{
		if (lastPos != Vector3.zero)
			transform.position -= topWave.position - lastPos;
		lastPos = topWave.position;
	}
}

