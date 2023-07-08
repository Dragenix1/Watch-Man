using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance { get; private set; }

    [SerializeField] private Vector2Int size;
    public Vector2Int Size { get { return size; } }
    private Vector2Int minSize = new Vector2Int(11, 15);
    private Vector2Int maxSize = new Vector2Int(51, 51);
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject cameraLane;
    public GameObject Player { get; private set; }
    private readonly List<int> possiblePositions = new();
    private readonly Vector2[] spawnPoints = new Vector2[3];
    private Vector2 endPoint;
    public Transform EndPoint 
    { get; private set; }

    private Transform[] cameraLanes;
    public Transform[] CameraLanes
    { get { return cameraLanes; } }

    private List<Vector2Int> possibleWayPoints = new();
    private List<Vector2Int> possibleTargets;
    private List<Transform> targets = new();
    public List<Transform> Targets { get { return targets; } }

    [SerializeField] private int targetAmount = 3;

    private List<Vector2Int> cameraPoints = new();

    private NavMeshSurface surface;
    private GameObject baseLevel;
    private GameObject regals;
    private GameObject cams;
    private Transform baseLevelTransform;
    private Transform regalsTransform;
    private Transform camsTransform;
    [SerializeField] int[] obstacleSizes;

    //int lol = 1000001;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        surface = GetComponent<NavMeshSurface>();
        baseLevel = new GameObject("baseLevel");
        baseLevelTransform = baseLevel.transform;

        regals = new GameObject("regals");
        regalsTransform = regals.transform;

        cams = new GameObject("cams");
        camsTransform = cams.transform;
    }

    void OnEnable()
    {
        SetSize();
        GenerateOutline();
        GenerateStart();
        GenerateEnd();
        for (int i = 4; i < size.y - 4; i++)
        {
            if (i % 2 != 0)
            {
                GenerateOneLine(i);
            }
        }

        SetTargets();
        SetCameraPoints();

        surface.BuildNavMesh();

        spawnPoints[0] = new(20, size.x * 0.25f * 10);
        spawnPoints[1] = new(20, size.x * 0.5f * 10);
        spawnPoints[2] = new(20, size.x * 0.75f * 10);

        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(spawnPoints[0].x, 1, spawnPoints[0].y), Quaternion.identity);
        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(spawnPoints[1].x, 1, spawnPoints[1].y), Quaternion.identity);
        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(spawnPoints[2].x, 1, spawnPoints[2].y), Quaternion.identity);

        endPoint = new((size.y - 3) * 10, size.x * 0.5f * 10);
        EndPoint = Instantiate(target, new Vector3(endPoint.x, 1, endPoint.y), Quaternion.identity).transform;
        Player = Instantiate(player, new Vector3(endPoint.x, 1, endPoint.y), Quaternion.Euler(0, -90, 0));
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

    private void GenerateOutline()
    {
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                Instantiate(plane, new Vector3(i * 10, 0, j * 10), Quaternion.identity, baseLevelTransform);
            }
        }

        for (int i = 0; i <= size.x - 1; i++)
        {
            Instantiate(wall, new Vector3(0, 25, i * 10), Quaternion.identity, baseLevelTransform);
            Instantiate(wall, new Vector3((size.y - 1) * 10, 25, i * 10), Quaternion.identity, baseLevelTransform);
        }

        for (int i = 1; i < size.y - 1; i++)
        {
            Instantiate(wall, new Vector3(i * 10, 25, 0), Quaternion.identity, baseLevelTransform);
            Instantiate(wall, new Vector3(i * 10, 25, (size.x - 1) * 10), Quaternion.identity, baseLevelTransform);
        }
    }

    private void GenerateStart()
    {
        GenerateOneLine(3);
    }

    private void GenerateEnd()
    {
        GenerateOneLine(size.y - 4);
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
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(lineNumber * 10, 5, (position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) * 10 - 2.5f), Quaternion.Euler(0, Random.Range(0, 4) * 90f, 0), regalsTransform);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(lineNumber * 10, 5, (position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) * 10 + 2.5f), Quaternion.Euler(0, Random.Range(0, 4) * 90f, 0), regalsTransform);
                //Debug.Log((position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) * 10);
            }
            else
            {
                if ((position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) != 0
                    && (position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) != size.x - 1)
                {
                    possibleWayPoints.Add(new Vector2Int(lineNumber * 10, (position + 2 - obstacleSizes[obstacleSizes.Length - 1] + i) * 10));
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
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(lineNumber * 10, 5, (position + 1 - i) * 10 - 2.5f), Quaternion.identity, regalsTransform);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(lineNumber * 10, 5, (position + 1 - i) * 10 + 2.5f), Quaternion.identity, regalsTransform);
            }
            else
            {
                if (position + 1 - i != 0 && position + 1 - i != size.x - 1)
                {
                    possibleWayPoints.Add(new Vector2Int(lineNumber * 10, (position + 1 - i) * 10));
                }
            }
            possiblePositions.Remove(position - i);
            //Debug.Log(position - i + 1);
        }
        for (int i = 1; i <= obstacleSize - leftDisplacement; i++)
        {
            if (i != obstacleSize - leftDisplacement)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(lineNumber * 10, 5, (position + 1 + i) * 10 - 2.5f), Quaternion.identity, regalsTransform);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(lineNumber * 10, 5, (position + 1 + i) * 10 + 2.5f), Quaternion.identity, regalsTransform);
            }
            else
            {
                if (position + 1 + i != 0 && position + 1 + i != size.x - 1)
                {
                    possibleWayPoints.Add(new Vector2Int(lineNumber * 10, (position + 1 + i) * 10));
                }
            }
            possiblePositions.Remove(position + i);
            //Debug.Log(position + i + 1);
        }
        //Debug.Log(lol++);
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
                    possibleTargets.Add(new Vector2Int(i * 10, j * 10));
                }
            }
        }

        GameObject newObject;
        for (int i = 0; i < targetAmount; i++)
        {
            int index = Random.Range(0, possibleTargets.Count);
            //newObject = new GameObject("Target");
            //newObject.transform.position = new Vector3(possibleTargets[index].x, 1, possibleTargets[index].y);
            newObject = Instantiate(target, new Vector3(possibleTargets[index].x, 1, possibleTargets[index].y), Quaternion.identity);
            targets.Add(newObject.transform);
            possibleTargets.RemoveAt(index);
        }

        //foreach (Vector2Int targetPos in possibleTargets)
        //{
        //    Instantiate(target, new Vector3(targetPos.x, 20, targetPos.y), Quaternion.identity);
        //}
    }

    private void SetCameraPoints()
    {
        for (int i = 2; i < size.y - 2; i++)
        {
            if (i % 2 == 0)
            {
                int value2 = (size.x - 9) / 4;
                value2++;

                for (int j = 0; j <= value2; j++)
                {
                    cameraPoints.Add(new Vector2Int(i * 10, (j * 4 + 2) * 10));
                }
            }
        }

        cameraLanes = new Transform[cameraPoints.Count];
        GameObject newObject;
        int index = 0;
        foreach (Vector2Int cam in cameraPoints)
        {
            newObject = Instantiate(cameraLane, new Vector3(cam.x, 20, cam.y), Quaternion.identity, camsTransform);
            //newObject = new GameObject("CameraLane");
            //newObject.transform.position = new Vector3(cam.x, 20, cam.y);
            //Instantiate(target, new Vector3(cam.x, 20, cam.y), Quaternion.identity);
            cameraLanes[index] = newObject.transform;
            index++;
        }
    }
}