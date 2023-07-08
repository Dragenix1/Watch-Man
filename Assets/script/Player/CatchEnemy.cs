using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchEnemy : MonoBehaviour, IPointBehaviour
{
    private PointSystemManager pointManager;

    private const int basePoints = 50;

    private void Start()
    {
        pointManager = PointSystemManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(other.gameObject);
            int pointsToReceive = (int)(basePoints * other.GetComponent<EnemyMovement>().Speed);
            DecreasePoints(pointsToReceive);
        }
    }

    public void DecreasePoints(int points)
    {
        pointManager.ValueOfStolenGoods -= points;
    }

    public void IncreasePoints(int points)
    {
        
    }
}
