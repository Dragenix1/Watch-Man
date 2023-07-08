using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private LevelGenerator levelGenerator;
    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        levelGenerator = LevelGenerator.Instance;
        transform.position = new Vector3((levelGenerator.Size.y - 1) * 5f, 500, (levelGenerator.Size.x - 1) * 5f);
        //camera.orthographicSize = 75;
    }
}
