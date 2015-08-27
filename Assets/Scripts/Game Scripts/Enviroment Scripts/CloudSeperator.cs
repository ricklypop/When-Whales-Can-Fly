using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSeperator : MonoBehaviour
{
    public ChunkHandler handler;
    public Transform trackObject;
    public float minDistance;

    void Update()
    {
        List<Chunk> renderChunk = handler.Chunks;
        foreach (Chunk chunk in renderChunk)
        {
            foreach (GameObject c in chunk.Objects)
            {
                foreach (GameObject c1 in chunk.Objects)
                {
                    if (c != null && c1 != null && c.GetComponent<Collider2D>() != null && c1.GetComponent<Collider2D>() != null && (c.GetComponent<Collider2D>().bounds.Intersects(c1.GetComponent<Collider2D>().bounds)
                        || Vector2.Distance(c.transform.position, c1.transform.position) < minDistance)
                        && c != c1)
                        seperate(c.transform, minDistance);
                }
            }
        }
    }

    void seperate(Transform c, float seperation)
    {
        float direction = c.position.x - trackObject.position.x;

        if (direction > 0)
            c.transform.position = new Vector3(c.transform.position.x + seperation, c.transform.position.y, c.transform.position.z);

        if (direction < 0)
            c.transform.position = new Vector3(c.transform.position.x - seperation, c.transform.position.y, c.transform.position.z);
    }
}
