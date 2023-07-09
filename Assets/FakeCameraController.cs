using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        transform.LookAt(target.position);
        transform.Rotate(Vector3.right, -90);
    }
}
