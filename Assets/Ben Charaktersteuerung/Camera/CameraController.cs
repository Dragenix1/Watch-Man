using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private LevelGenerator levelGenerator;

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
    public bool coroutineIsRunning = false;



    // Start is called before the first frame update
    void Start()
    {
        levelGenerator = LevelGenerator.Instance;
        cameraLanes = levelGenerator.CameraLanes;
        cameraRotation = transform.rotation;
        activeCameraLane = cameraLanes.Length - 1;
        transform.position = cameraLanes[activeCameraLane].position;
        cameraFocus = levelGenerator.Player;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cameraFocus.transform);

        playerPos = cameraFocus.transform.position;

        if (!coroutineIsRunning)
        {
            float distance = Vector3.Distance(playerPos, cameraLanes[activeCameraLane].position);
            int index = 0;
            foreach (Transform lane in cameraLanes)
            {
                if (distance > Vector3.Distance(playerPos, lane.position))
                {
                    StartCoroutine(MovingCamera(new Vector3(lane.position.x, transform.position.y, lane.position.z + cameraOffset)));
                    //transform.position = new Vector3(lane.position.x, transform.position.y, lane.position.z + cameraOffset);
                    //transform.position = newPos;
                    //coroutineCam = StartCoroutine(MovingCameraX(newPos));
                    activeCameraLane = index;
                    return;
                }
                index++;
            }
        }

        ////UP/DOWN Movement Camera
        //if (!coroutineIsRunning)
        //{
        //    if (playerPos.z >= (lane2Z + cameraOffset) && transform.position.z != lane2Z)
        //    {
        //        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, lane2Z);
        //        //coroutineCam = StartCoroutine(MovingCameraZ(newPos));
        //        transform.position = newPos;
        //    }
        //    else if (playerPos.z < (lane2Z + cameraOffset) && transform.position.z != lane1Z)
        //    {
        //        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, lane1Z);
        //        //coroutineCam = StartCoroutine(MovingCameraZ(newPos));
        //        transform.position = newPos;
        //    }
        //}

        //float distance = Vector3.Distance(playerPos, cameraLanes[activeCameraLane].position);
        //int index = 0;
        ////LEFT/RIGHT Movement Camera
        //foreach (Transform lane in cameraLanes)
        //{
        //    if (distance > Vector3.Distance(playerPos, lane.position)) 
        //    {
        //        Vector3 newPos = new Vector3(lane.position.x, transform.position.y, transform.position.z);
        //        transform.position = newPos;
        //        //coroutineCam = StartCoroutine(MovingCameraX(newPos));
        //        activeCameraLane = index;
        //        return;
        //    }
        //    index++;
        //}

    }

    IEnumerator MovingCamera(Vector3 newPos)
    {
        coroutineIsRunning = true;
        while (!Mathf.Approximately(transform.position.z, newPos.z))
        {
            Vector3 pos = transform.position;
            float step = cameraSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(pos, newPos, step);
            yield return new WaitForSeconds(0.00001f);
        }

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

    //IEnumerator MovingCameraZ(Vector3 newPos)
    //{
    //    coroutineIsRunning = true;

    //    //while (transform.position != newPos)
    //    while (!Mathf.Approximately(transform.position.z, newPos.z))
    //    {
    //        Vector3 pos = transform.position;
    //        float step = cameraSpeed * Time.deltaTime;
    //        transform.position = Vector3.Lerp(pos, newPos, step);
    //        yield return new WaitForSeconds(0.00001f);
    //    }
    //    coroutineIsRunning = false;
    //    yield return null;
    //}

    //IEnumerator MovingCameraX(Vector3 newPos)
    //{
    //    coroutineIsRunning = true;

    //    //while (transform.position != newPos)
    //    while (!Mathf.Approximately(transform.position.x, newPos.x))
    //    {
    //        Vector3 pos = transform.position;
    //        float step = cameraSpeed * Time.deltaTime;
    //        transform.position = Vector3.Lerp(pos, newPos, step);
    //        yield return new WaitForSeconds(0.00001f);
    //    }
    //    coroutineIsRunning = false;
    //    yield return null;
    //}
}
