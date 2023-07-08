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
            //In Destroy der Gegner vllt noch Catchsound abspielen
            IncreasePoints(pointsToReceive);
        }
    }

    public void DecreasePoints(int points)
    {
        pointManager.PlayerPoints -= points;
    }

    public void IncreasePoints(int points)
    {
        pointManager.PlayerPoints += points;
    }
}
