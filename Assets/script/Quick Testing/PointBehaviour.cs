using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehaviour : MonoBehaviour, IPointBehaviour
{
    private PointSystemManager pointManager;

    private float timer;

    private void Start()
    {
        pointManager = PointSystemManager.Instance;
        timer = 1f;
    }

    //Both Point methods are supposed to be called upon point relevant action
    public void IncreasePoints(int points)
    {
        pointManager.ValueOfStolenGoods += points;
    }

    public void DecreasePoints(int points)
    {
        pointManager.ValueOfStolenGoods -= points;
    }
}
