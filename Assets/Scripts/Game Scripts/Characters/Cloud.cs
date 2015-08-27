using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{
    private bool check;
    private ChunkHandler handler;
    private Chunk chunk;

    public float maxSize;
    public float minSize;

    void Start()
    {
        transform.localScale = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), 1);
    }

    void Update()
    {
        if (handler == null)
            handler = GetComponent<Handler>().handler;

        chunk = GetComponent<Entity>().currrentChunk;
    }

    public ChunkHandler Handler
    {
        get
        {
            return handler;
        }

        set
        {
            handler = value;
        }
    }

    public bool Check
    {
        get
        {
            return check;
        }

        set
        {
            check = value;
        }
    }
}
