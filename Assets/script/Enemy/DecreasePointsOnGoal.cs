using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecreasePointsOnGoal : MonoBehaviour, IPointBehaviour
{
    private PointSystemManager pointManager;

    private const int basePoints = 50;
    private const float slowEnemy = 4.0f;
    private const float fastEnemy = 8.0f;

    private const float lowPointPenalty = 1.5f;
    private const float normalPointPenalty = 2.0f;
    private const float highPointPenalty = 3.0f;

    private void Start()
    {
        pointManager = PointSystemManager.Instance;
    }

    private void OnDestroy()
    {
        if(GetComponent<EnemyMovement>().GoalReached)
        {
            DecreasePoints(basePoints);
        }
    }

    public void DecreasePoints(int points)
    {
        float enemySpeed = GetComponent<EnemyMovement>().Speed;
        float pointPenalty = 0;
        if(enemySpeed < slowEnemy)
        {
            pointPenalty = highPointPenalty;
        }
        else if (enemySpeed > slowEnemy && enemySpeed < fastEnemy)
        {
            pointPenalty = normalPointPenalty;
        }
        else if(enemySpeed > fastEnemy)
        {
            pointPenalty = lowPointPenalty;
        }

        int pointsToReceive = (int)(basePoints * pointPenalty);
        pointManager.PlayerPoints -= pointsToReceive;
    }

    public void IncreasePoints(int points)
    {
        
    }
}
