using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControls : MonoBehaviour
{
    private void Start()
    {
        transform.LookAt(transform.parent);
    }
}
