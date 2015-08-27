using UnityEngine;
using UnityEngine.UI;

public class SetMoveScore : MonoBehaviour {
    private BasicSettings settings;
    private Vector3 lastPos;
    private int textLength;
    public float size;
    public Transform trackObject;

        void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<BasicSettings>();
    }

	void Update () {
        int currentScore = Mathf.RoundToInt(settings.currentScore);
        GetComponent<Text>().text = "Distance: " + currentScore.ToString();
        int currentLength = GetComponent<Text>().text.Length;
        if (currentLength != textLength)
        {
            transform.position -= new Vector3((size * (currentLength - textLength)), 0, 0);
            textLength = currentLength;
        }
        lastPos = trackObject.position;
	}
}
