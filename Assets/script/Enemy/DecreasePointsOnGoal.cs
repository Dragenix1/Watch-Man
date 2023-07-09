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
    private EnemyMovement enemyMovement;

    private void Start()
    {
        pointManager = PointSystemManager.Instance;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnDestroy()
    {
        if(enemyMovement.GoalReached)
        {
            IncreasePoints(basePoints);
            pointManager.ValueOfEscaped++;
        }
        else
        {
            pointManager.ValueOfCatches++;
        }
    }

    public void DecreasePoints(int points)
    {
        
    }

    public void IncreasePoints(int points)
    {
        float enemySpeed = enemyMovement.Speed;
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

        int pointsToReceive = (int)(basePoints * pointPenalty * enemyMovement.WaypointAmount);
        pointManager.ValueOfStolenGoods += pointsToReceive;
    }
}
