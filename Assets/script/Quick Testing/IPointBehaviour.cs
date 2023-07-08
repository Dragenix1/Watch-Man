using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPointBehaviour
{
    public abstract void IncreasePoints(int points);

    public abstract void DecreasePoints(int points);
}
