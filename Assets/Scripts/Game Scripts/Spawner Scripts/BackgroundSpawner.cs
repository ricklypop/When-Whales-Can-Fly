using UnityEngine;
using System.Collections;

public class BackgroundSpawner : MonoBehaviour
{
    private ChunkHandler handler;
    private Chunk currentChunk;
    private Transform trackObject;
    private CloudSeperator cloudSep;
    private float maxTime;
    private float time;

    public int chunkSize;
    public Transform background;
    public Transform backCloud;
    public float backCloudHeight;

    void Start()
    {
        handler = new ChunkHandler(chunkSize);
        trackObject = Camera.main.transform;
        maxTime = Random.Range(1f, 2f);
        handler.AutoDestroyLeft = true;
        handler.AutoDestroyDown = true;
        handler.AutoDestroyRight = true;
        handler.AutoDestroyUp = true;
        cloudSep = gameObject.AddComponent<CloudSeperator>();
        cloudSep.handler = handler;
        cloudSep.trackObject = trackObject;
        cloudSep.minDistance = 3;
    }

    void Update()
    {
        if (currentChunk == null || !currentChunk.inChunk(trackObject.position))
        {
            //Get current chunk
            currentChunk = handler.getChunk(trackObject.position);
            //Make sure all render chunks have been created
            handler.getRenderChunks(new Vector2(trackObject.position.x, trackObject.position.y));
            //Dispose of left chunks
            handler.destoryChunksDirection("left", trackObject.position);
            handler.destoryChunksDirection("right", trackObject.position);
            handler.destoryChunksDirection("down", trackObject.position);
            handler.destoryChunksDirection("up", trackObject.position);
            //Fill empty chunks if they meet hieght requirement
            foreach (Chunk chunk in handler.getEmptyChunks())
            {
                Populate(chunk, true);
            }

            //This is going to look confusing, it is to check if there is a background in the chunk
            foreach (Chunk chunk in handler.getRenderChunks(trackObject.position))
            {
                bool hasBackground = false;
                foreach (GameObject obj in chunk.Objects)
                    if (obj != null && obj.GetComponent<BoxCollider2D>() == null)
                        hasBackground = true;

                if (!hasBackground)
                    Populate(chunk, false);
            }
        }

        time += Time.deltaTime;
        if(time > maxTime)
        {
            maxTime = Random.Range(1f, 2f);
            time = 0;
            newCloud();
        }
    }

    void newCloud()
    {
        float x = Random.Range(handler.getChunk(new Vector3(trackObject.transform.position.x + (chunkSize * 2), trackObject.transform.position.y, 1)).ChunkRect.x, handler.getChunk(new Vector3(trackObject.transform.position.x + (chunkSize * 3), trackObject.transform.position.y, 1)).ChunkRect.x);
        float y = Random.Range(handler.getChunk(new Vector3(trackObject.transform.position.x, trackObject.transform.position.y - chunkSize, 1)).ChunkRect.y, handler.getChunk(new Vector3(trackObject.transform.position.x, trackObject.transform.position.y + (chunkSize * 2), 1)).ChunkRect.y);
        if (y > backCloudHeight)
        {
            Transform cloud = (Transform)Instantiate(backCloud, new Vector3(0, 0, 10), Quaternion.identity);
            handler.getChunk(new Vector2(x, y)).Objects.Add(cloud.gameObject);
            cloud.GetComponent<RandomSpawn>().setSpawn(handler.getChunk(new Vector2(x, y)).ChunkRect);
            cloud.GetComponent<Handler>().handler = handler;
            cloud.GetComponent<Handler>().trackObject = trackObject;
        }
    }

    void Populate(Chunk chunk, bool spawnClouds)
    {
        Transform obj = (Transform)Instantiate(background, new Vector3(chunk.ChunkRect.x + (chunkSize / 2), chunk.ChunkRect.y + (chunkSize / 2), 10), Quaternion.identity);
        chunk.Objects.Add(obj.gameObject);
        
        for(int i = Random.Range(0, 2); i < 3; i++)
        {
            if (chunk.ChunkRect.y > backCloudHeight && spawnClouds)
            {
                Transform cloud = (Transform)Instantiate(backCloud, new Vector3(0, 0, 10), Quaternion.identity);
                chunk.Objects.Add(cloud.gameObject);
                cloud.GetComponent<RandomSpawn>().setSpawn(chunk.ChunkRect);
                cloud.GetComponent<Handler>().handler = handler;
                cloud.GetComponent<Handler>().trackObject = trackObject;
            }
        }
    }
}
