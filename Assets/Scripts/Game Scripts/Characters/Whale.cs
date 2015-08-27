using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour
{
    private bool grounded;
    private BasicSettings settings;
    private bool created;
    private Vector3 lastPos;
    private ChunkHandler handler;
    private float startPosition;
    private float farthestPosition;

    public GameObject cloud;
    public Transform UI;

    void Start()
    {
        Camera.main.GetComponent<CameraMovement>().trackObject = transform;
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<BasicSettings>();
        handler = GameObject.FindGameObjectWithTag("Spawner").GetComponent<CloudSpawner>().Handler;
    }

    void Update()
    {
        //Make sure game starts
        if(GetComponent<Rigidbody2D>().velocity.y < 0 && !settings.gameStart && transform.position.y <= settings.forceStartHeight)
        {
                cloud = GameObject.FindGameObjectWithTag("FirstCloud");
                GetComponent<Rigidbody2D>().isKinematic = true;
                settings.gameStart = true;
                startPosition = transform.position.x;
                GameObject.FindGameObjectWithTag("ControlScheme").GetComponent<PressAndDrag>().rigidBody = GetComponent<Rigidbody2D>();
        }

        //Pre-Gameover Actions
        if (settings.gameStart && transform.position.y < settings.gameOverHeight && !created)
        {
            GameObject.FindGameObjectWithTag("Wave").GetComponent<RemoveFromParent>().reset();
            Instantiate(UI, new Vector3(0, 0, 0), Quaternion.identity);
            created = true;
        }

        //Gameover
        if (transform.position.y < 0)
        {
            GameObject.Find("Scorescreen").GetComponent<ShowHideScore>().show = false;
            settings.gameStart = false;
            Destroy(gameObject);
        }

        //Move with cloud
        if (cloud != null && lastPos != Vector3.zero && lastPos != cloud.transform.position)
        {
            transform.position += cloud.transform.position - lastPos;
            startPosition += cloud.transform.position.x - lastPos.x;
        }

        if (cloud != null)
            lastPos = cloud.transform.position;
        else
            lastPos = Vector3.zero;

        if (!GetComponent<Rigidbody2D>().isKinematic)
            lastPos = Vector3.zero;

        //Score Tracking
        if (transform.position.x > farthestPosition && settings.gameStart)
            farthestPosition = transform.position.x;

        settings.currentScore = farthestPosition - startPosition;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        grounded = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        foreach (GameObject obj in handler.getChunk(transform.position).Objects)
        {
            if (obj != null && col.gameObject != null && col == obj.GetComponent<Collider2D>())
            {
                grounded = true;
                if (cloud != col.gameObject && GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    cloud = col.gameObject;
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    if (!settings.gameStart)
                    {
                        settings.gameStart = true;
                        startPosition = transform.position.x;
                        GameObject.FindGameObjectWithTag("ControlScheme").GetComponent<PressAndDrag>().rigidBody = GetComponent<Rigidbody2D>();
                    }
                }
            }
        }
    }
}
