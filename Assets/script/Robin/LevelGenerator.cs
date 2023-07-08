using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

//public enum Fill
//{
//    Wall,
//    NextToWall,
//    Start,
//    End,
//    Empty
//}

//public struct Level
//{
//    public Fill fill;
//    public bool isFilled;
//}

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    private Vector2Int minSize = new Vector2Int(11, 15);
    private Vector2Int maxSize = new Vector2Int(51, 51);
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject[] obstacles;
    private readonly List<int> possiblePositions = new();
    private readonly Vector2[] spawnPoints = new Vector2[3];
    private Vector2 endPoint;

    private List<Vector2Int> possibleWayPoints = new();
    private List<Vector2Int> possibleTargets;
    private List<Vector2Int> targets = new();
    [SerializeField] private int targetAmount = 3;
    public GameObject target;

    private List<Vector2Int> cameraPoints = new();

    private NavMeshSurface surface;
    private GameObject baseLevel;
    private Transform baseLevelTransform;
    [SerializeField] int[] obstacleSizes;

    int lol = 1000001;

    //private GameObject newObject;
    //private Level[,] level;

    private void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
        baseLevel = Instantiate(new GameObject("baseLevel"), Vector3.zero, Quaternion.identity);
        baseLevelTransform = baseLevel.transform;
        Array.Sort(obstacleSizes);
    }

    void Start()
    {
        SetSize();

        GenerateOutline();
        GenerateStart();
        GenerateEnd();
        for (int i = 6; i < size.y - 6; i++)
        {
            if (i % 2 != 0)
            {
                GenerateOneLine(i);
            }
        }

        spawnPoints[0] = new (size.x * 0.25f * 10, 20);
        spawnPoints[1] = new (size.x * 0.5f * 10, 20);
        spawnPoints[2] = new (size.x * 0.75f * 10, 20);

        //Instantiate(wall, new Vector3(spawnPoints[0].x, 20 ,spawnPoints[0].y), Quaternion.identity);
        //Instantiate(wall, new Vector3(spawnPoints[1].x, 20 ,spawnPoints[1].y), Quaternion.identity);
        //Instantiate(wall, new Vector3(spawnPoints[2].x, 20 ,spawnPoints[2].y), Quaternion.identity);

        endPoint = new(size.y * 0.5f * 10, (size.y - 3) * 10);
        //Instantiate(wall, new Vector3(endPoint.x, 20, endPoint.y), Quaternion.identity);

        SetTargets();
        SetCameraPoints();

        surface.BuildNavMesh();
    }

    private void SetCameraPoints()
    {
        for (int i = 4; i < size.y - 4; i++)
        {
            if (i % 2 == 0)
            {
                int value2 = (size.x - 9) / 4;
                value2++;

                for (int j = 0; j <= value2; j++)
                {
                    cameraPoints.Add(new Vector2Int((j * 4 + 2) * 10, i * 10));
                }
            }
        }

        //foreach(Vector2Int cam in cameraPoints)
        //{
        //    Instantiate(target, new Vector3(cam.x, 20, cam.y), Quaternion.identity);
        //}
    }

    private void SetTargets()
    {
        possibleTargets = possibleWayPoints;
        for (int i = 6; i < size.y - 6; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 1; j < size.x - 1; j++)
                {
                    possibleTargets.Add(new Vector2Int(j * 10, i * 10));
                }
            }
        }

        for (int i = 0; i < targetAmount; i++)
        {
            int index = Random.Range(0, possibleTargets.Count);
            targets.Add(possibleTargets[index]);
            possibleTargets.RemoveAt(index);
        }

        //foreach (Vector2Int targetPos in possibleTargets)
        //{
        //    Instantiate(target, new Vector3(targetPos.x, 20, targetPos.y), Quaternion.identity);
        //}
    }

    private void SetSize()
    {
        if (size.x <= minSize.x) size.x = minSize.x;
        if (size.y <= minSize.y) size.y = minSize.y;
        if (size.x >= maxSize.x) size.x = maxSize.x;
        if (size.y >= maxSize.y) size.y = maxSize.y;
        if (size.x % 2 == 0) size.x++;
        if (size.y % 2 == 0) size.y++;
    }

    private void GenerateStart()
    {
        GenerateOneLine(5);
    }
    
    private void GenerateEnd()
    {
        GenerateOneLine(size.y - 6);
    }

    private int SearchLeft(int position)
    {
        int obstacleSize = 1;
        while (possiblePositions.Contains(position - obstacleSize))
        {
            obstacleSize++;
        }
        return obstacleSize;
    }

    private int SearchRight(int position, int obstacleSize)
    {
        int counter = 1;
        while (possiblePositions.Contains(position + counter))
        {
            obstacleSize++;
            counter++;
        }

        return obstacleSize;
    }

    private void SetBiggestObstacle(int position, int lineNumber)
    {
        for (int i = -1; i <= obstacleSizes[obstacleSizes.Length - 1]; i++)
        {
            if (i != -1 && i != obstacleSizes[obstacleSizes.Length - 1])
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3((position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) * 10, 10, lineNumber * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90f, 0));
            }
            else
            {
                if ((position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) != 0 
                    && (position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) != size.x - 1)
                {
                    possibleWayPoints.Add(new Vector2Int((position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) * 10, lineNumber * 10));
                }
            }
            possiblePositions.Remove(position - i);
            //Debug.Log(position - i + 1);
        }
        //Debug.Log(lol++);
    }

    private void SetObstacle(int position, int obstacleSize, int lineNumber, int leftDisplacement)
    {
        int oldObstacleSize = obstacleSize;
        for (int i = 0; i <= obstacleSizes.Length - 1; i++)
        {
            if (oldObstacleSize >= obstacleSizes[i])
            {
                obstacleSize = obstacleSizes[i];
            }
        }

        for (int i = 0; i <= leftDisplacement + 1; i++)
        {
            if (i != leftDisplacement + 1)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3((position + 1 - i) * 10, 10, lineNumber * 10), Quaternion.identity);
            }
            else
            {
                if (position + 1 - i != 0 && position + 1 - i != size.x - 1)
                {
                    possibleWayPoints.Add(new Vector2Int((position + 1 - i) * 10, lineNumber * 10));
                }
            }
            possiblePositions.Remove(position - i);
            //Debug.Log(position - i + 1);
        }
        for (int i = 1; i <= obstacleSize - leftDisplacement; i++)
        {
            if (i != obstacleSize - leftDisplacement)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3((position + 1 + i) * 10, 10, lineNumber * 10), Quaternion.identity);
            }
            else
            {
                if (position + 1 + i != 0 && position + 1 + i != size.x - 1)
                {
                    possibleWayPoints.Add(new Vector2Int((position + 1 + i) * 10, lineNumber * 10));
                }
            }
            possiblePositions.Remove(position + i);
            //Debug.Log(position + i + 1);
        }
        //Debug.Log(lol++);
    }

    private void GenerateOneLine(int lineNumber)
    {
        for (int i = 0; i < size.x - 2; i++)
        {
            possiblePositions.Add(i);
        }

        while (possiblePositions.Count != 0)
        {
            int position = possiblePositions[Random.Range(0, possiblePositions.Count)];
            int obstacleSize = SearchLeft(position);

            if (obstacleSize >= obstacleSizes[obstacleSizes.Length - 1])
            {
                SetBiggestObstacle(position, lineNumber);
            }
            else
            {
                int leftDisplacement = obstacleSize - 1;
                obstacleSize = SearchRight(position, obstacleSize);
                SetObstacle(position, obstacleSize, lineNumber, leftDisplacement);
            }
        }

        //Debug.Log("EndLine" + lineNumber);
    }

    private void GenerateOutline()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Instantiate(plane, new Vector3(i * 10, 0, j * 10), Quaternion.identity, baseLevelTransform);
            }
        }

        for (int i = 0; i <= size.x - 1; i++)
        {
            Instantiate(wall, new Vector3(i * 10, 25, 0), Quaternion.identity, baseLevelTransform);
            Instantiate(wall, new Vector3(i * 10, 25, (size.y - 1) * 10), Quaternion.identity, baseLevelTransform);
        }

        for (int i = 1; i < size.y - 1; i++)
        {
            Instantiate(wall, new Vector3(0, 25, i * 10), Quaternion.identity, baseLevelTransform);
            Instantiate(wall, new Vector3((size.x - 1) * 10, 25, i * 10), Quaternion.identity, baseLevelTransform);
        }
    }
}