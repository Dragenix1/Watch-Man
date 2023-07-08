using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraFocus;
    private Vector3 playerPos;

    public Transform[] cameraLanes;
    public int activeCameraLane;

    public float cameraOffset = 5.0f;
    public float cameraSpeed = 2.0f;

    public float lane1Z = -30.0f;
    public float lane2Z = 10.0f;


    private Quaternion cameraRotation;
    private Coroutine coroutineCam;
    private bool coroutineIsRunning = false;



    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = transform.rotation;
        activeCameraLane = 0;
        transform.position = cameraLanes[activeCameraLane].position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cameraFocus.transform);

        playerPos = cameraFocus.transform.position;

        //UP/DOWN Movement Camera
        if (!coroutineIsRunning)
        {
            if (playerPos.z >= (lane2Z + cameraOffset) && transform.position.z != lane2Z)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, lane2Z);
                coroutineCam = StartCoroutine(MovingCameraZ(newPos));
            }
            else if (playerPos.z < (lane2Z + cameraOffset) && transform.position.z != lane1Z)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, lane1Z);
                coroutineCam = StartCoroutine(MovingCameraZ(newPos));
            }
        }

        float distance = Vector3.Distance(playerPos, cameraLanes[activeCameraLane].position);
        int index = 0;
        //LEFT/RIGHT Movement Camera
        foreach (Transform lane in cameraLanes)
        {
            if (distance > Vector3.Distance(playerPos, lane.position)) 
            {
                Vector3 newPos = new Vector3(lane.position.x, transform.position.y, transform.position.z);
                coroutineCam = StartCoroutine(MovingCameraX(newPos));
                activeCameraLane = index;
                return;
            }
            index++;
        }

    }

    IEnumerator MovingCameraZ(Vector3 newPos)
    {
        coroutineIsRunning = true;

        //while (transform.position != newPos)
        while (!Mathf.Approximately(transform.position.z, newPos.z))
        {
            Vector3 pos = transform.position;
            float step = cameraSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(pos, newPos, step);
            yield return new WaitForSeconds(0.00001f);
        }
        coroutineIsRunning = false;
        yield return null;
    }

    IEnumerator MovingCameraX(Vector3 newPos)
    {
        coroutineIsRunning = true;

        //while (transform.position != newPos)
        while (!Mathf.Approximately(transform.position.x, newPos.x))
        {
            Vector3 pos = transform.position;
            float step = cameraSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(pos, newPos, step);
            yield return new WaitForSeconds(0.00001f);
        }
        coroutineIsRunning = false;
        yield return null;
    }
}
