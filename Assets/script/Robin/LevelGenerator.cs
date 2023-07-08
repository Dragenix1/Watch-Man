using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Fill
{
    Wall,
    NextToWall,
    Start,
    End,
    Empty
}

public struct Level
{
    public Fill fill;
    public bool isFilled;
}
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject bigObstacle;
    [SerializeField] private GameObject midObstacle;
    [SerializeField] private GameObject smallObstacle;
    private GameObject newObject;
    private Vector2Int startPoint;
    private Level[,] level;
    private List<int> possiblePositions = new List<int>();

    [SerializeField] int maxObstacleSize = 3;
    [SerializeField] int midObstacleSize = 2;

    // Start is called before the first frame update
    void Start()
    {
        if (size.x <= 5) size.x = 5;
        if (size.x <= 5) size.x = 5;
        if (size.x % 2 == 0) size.x++;
        if (size.y % 2 == 0) size.y++;

        level = new Level[size.x, size.y];

        GenerateOutline();
        GenerateStart();
        GenerateEnd();
    }

    private void GenerateStart()
    {
        for (int i = 0; i < size.x - 2; i++)
        {
            possiblePositions.Add(i);
        }
        
        int leftDisplacement;

        while (possiblePositions.Count != 0)
        {
            int position = possiblePositions[UnityEngine.Random.Range(0, possiblePositions.Count)];
            int counter = 1;
            int obstacleSize = 1;

            while (possiblePositions.Contains(position - counter))
            {
                obstacleSize++;
                counter++;
            }

            //if (obstacleSize >= maxObstacleSize)
            //{
            //    Instantiate(bigObstacle, new Vector3(position + 1 - maxObstacleSize, 0.5f, 3), Quaternion.identity);
            //    for (int i = -1; i <= maxObstacleSize; i++)
            //    {
            //        possiblePositions.Remove(position - i);
            //    }
            //}

            if (obstacleSize >= midObstacleSize)
            {
                Instantiate(midObstacle, new Vector3(position + 1 - midObstacleSize, 0.5f, 3), Quaternion.identity);
                for (int i = -1; i <= midObstacleSize; i++)
                {
                    possiblePositions.Remove(position - i);
                }
            }
            else
            {
                leftDisplacement = obstacleSize - 1;

                counter = 1;
                while (possiblePositions.Contains(position + counter))
                {
                    obstacleSize++;
                    counter++;
                }

                if (obstacleSize >= maxObstacleSize)
                {
                    Instantiate(bigObstacle, new Vector3(position + 1 - leftDisplacement, 0.5f, 3), Quaternion.identity);
                    obstacleSize = maxObstacleSize;

                }
                else if (obstacleSize >= midObstacleSize)
                {
                    Instantiate(midObstacle, new Vector3(position + 1 - leftDisplacement, 0.5f, 3), Quaternion.identity);
                    obstacleSize = midObstacleSize;
                }
                else
                {
                    Instantiate(smallObstacle, new Vector3(position + 1, 0.5f, 3), Quaternion.identity);
                }

                for (int i = -1; i < leftDisplacement; i++)
                {
                    possiblePositions.Remove(position - i);
                }
                for (int i = -1; i <= obstacleSize - leftDisplacement; i++)
                {
                    possiblePositions.Remove(position + i);
                }
            }
        }
    }
    
    private void GenerateEnd()
    {

    }

    private void GenerateOutline()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                level[i, j].isFilled = false;
                level[i, j].fill = Fill.Empty;
            }
        }

        newObject = Instantiate(plane, new Vector3(size.x / 2, 0, size.y / 2), Quaternion.identity);
        newObject.transform.localScale = new Vector3(size.x / 10.0f, 1, size.y / 10.0f);

        for (int i = 0; i <= size.x - 1; i++)
        {
            Instantiate(wall, new Vector3(i, 0.5f, 0), Quaternion.identity);
            level[i, 0].isFilled = true;
            level[i, 0].fill = Fill.Wall;
            level[i, 1].isFilled = true;
            level[i, 1].fill = Fill.Start;
            level[i, 2].isFilled = true;
            level[i, 2].fill = Fill.Start;
            Instantiate(wall, new Vector3(i, 0.5f, size.y - 1), Quaternion.identity);
            level[i, size.y - 1].isFilled = true;
            level[i, size.y - 1].fill = Fill.Wall;
            level[i, size.y - 2].isFilled = true;
            level[i, size.y - 2].fill = Fill.End;
            level[i, size.y - 3].isFilled = true;
            level[i, size.y - 3].fill = Fill.End;
        }

        for (int i = 1; i < size.y - 1; i++)
        {
            Instantiate(wall, new Vector3(0, 0.5f, i), Quaternion.identity);
            level[0, size.y - 1].isFilled = true;
            level[0, size.y - 1].fill = Fill.Wall;
            Instantiate(wall, new Vector3(size.x - 1, 0.5f, i), Quaternion.identity);
            level[size.x - 1, size.y - 1].isFilled = true;
            level[size.x - 1, size.y - 1].fill = Fill.Wall;
        }
    }

}