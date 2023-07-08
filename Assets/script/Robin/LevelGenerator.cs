using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private GameObject[] smallObstacle;
    private GameObject newObject;
    private Vector2Int startPoint;
    private Level[,] level;
    private List<int> possiblePositions = new List<int>();

    private NavMeshSurface surface;
    private GameObject baseLevel;
    private Transform baseLevelTransform;
    [SerializeField] int maxObstacleSize = 5;
    [SerializeField] int midObstacleSize = 2;
    [SerializeField] int smallObstacleSize = 1;

    private void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
        baseLevel = Instantiate(new GameObject("baseLevel"), Vector3.zero, Quaternion.identity);
        baseLevelTransform = baseLevel.transform;
    }
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
        for (int i = 4; i < size.y - 4; i++)
        {
            if (i % 2 != 0)
            {
                GenerateOneLine(i);
            }
        }

        surface.BuildNavMesh();
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

            if (obstacleSize >= maxObstacleSize)
            {
                for (int i = -1; i <= maxObstacleSize; i++)
                {
                    if (i != -1 && i != maxObstacleSize)
                    {
                        Instantiate(smallObstacle[Random.Range(0, smallObstacle.Length)], new Vector3((position + 2 - maxObstacleSize + i) * 10, 10, lineNumber * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90f, 0));
                    }
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
                    obstacleSize = maxObstacleSize;

                }
                else if (obstacleSize >= midObstacleSize)
                {
                    obstacleSize = midObstacleSize;
                }
                else
                {
                    obstacleSize = smallObstacleSize;
                }

                for (int i = 0; i <= leftDisplacement + 1; i++)
                {
                    if (i != leftDisplacement + 1)
                    {
                        Instantiate(smallObstacle[Random.Range(0, smallObstacle.Length)], new Vector3((position + 1 - i) * 10, 10, lineNumber * 10), Quaternion.identity);
                    }
                    possiblePositions.Remove(position - i);
                }
                for (int i = 1; i <= obstacleSize - leftDisplacement; i++)
                {
                    if(i != obstacleSize - leftDisplacement)
                    {
                        Instantiate(smallObstacle[Random.Range(0, smallObstacle.Length)], new Vector3((position + 1 + i) * 10, 10, lineNumber * 10), Quaternion.identity);
                    }
                    possiblePositions.Remove(position + i);
                }
            }
        }
    }

    private void GenerateOutline()
    {
        //for (int i = 0; i < size.x; i++)
        //{
        //    for (int j = 0; j < size.y; j++)
        //    {
        //        level[i, j].isFilled = false;
        //        level[i, j].fill = Fill.Empty;
        //    }
        //}
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                newObject = Instantiate(plane, new Vector3(i * 10, 0, j * 10), Quaternion.identity, baseLevelTransform);
            }
        }

        for (int i = 0; i <= size.x - 1; i++)
        {
            Instantiate(wall, new Vector3(i * 10, 25, 0), Quaternion.identity, baseLevelTransform);
            //level[i, 0].isFilled = true;
            //level[i, 0].fill = Fill.Wall;
            //level[i, 1].isFilled = true;
            //level[i, 1].fill = Fill.Start;
            //level[i, 2].isFilled = true;
            //level[i, 2].fill = Fill.Start;
            Instantiate(wall, new Vector3(i * 10, 25, (size.y - 1) * 10), Quaternion.identity, baseLevelTransform);
            //level[i, size.y - 1].isFilled = true;
            //level[i, size.y - 1].fill = Fill.Wall;
            //level[i, size.y - 2].isFilled = true;
            //level[i, size.y - 2].fill = Fill.End;
            //level[i, size.y - 3].isFilled = true;
            //level[i, size.y - 3].fill = Fill.End;
        }

        for (int i = 1; i < size.y - 1; i++)
        {
            Instantiate(wall, new Vector3(0, 25, i * 10), Quaternion.identity, baseLevelTransform);
            //level[0, size.y - 1].isFilled = true;
            //level[0, size.y - 1].fill = Fill.Wall;
            Instantiate(wall, new Vector3((size.x - 1) * 10, 25, i * 10), Quaternion.identity, baseLevelTransform);
            //level[size.x - 1, size.y - 1].isFilled = true;
            //level[size.x - 1, size.y - 1].fill = Fill.Wall;
        }
    }

}