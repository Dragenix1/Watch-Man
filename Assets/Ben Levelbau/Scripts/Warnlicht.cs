using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warnlicht : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public float timeAfterStart = 20.0f;

    public float timer;

    private void Start()
    {
        timer = 0.0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        if (timer > timeAfterStart)
            Destroy(gameObject);
    }
}
