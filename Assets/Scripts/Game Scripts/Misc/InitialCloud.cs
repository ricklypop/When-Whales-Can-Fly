using UnityEngine;
using System.Collections;

public class InitialCloud : MonoBehaviour {
    private bool complete;

    void Start()
    {
        GetComponent<Cloud>().Check = true;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        GetComponent<Handler>().trackObject = Camera.main.transform;
        GetComponent<Handler>().handler = GameObject.FindGameObjectWithTag("Spawner").GetComponent<CloudSpawner>().Handler;
    }

    void Update()
    {
        if (!complete)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Chunk chunk = GetComponent<Cloud>().Handler.getChunk(transform.position);
            chunk.Objects.Add(gameObject);
            complete = true;
        }
    }
}
