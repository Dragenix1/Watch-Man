using UnityEngine;

public class LookAtMaincam : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform.position);
        transform.Rotate(Vector3.up, 180);
    }
}
