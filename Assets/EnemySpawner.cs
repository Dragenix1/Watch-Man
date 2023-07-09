using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private int startAmount;
    [SerializeField] private float increaseAmountTime;

    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private Transform endPoint;

    private GameObject enemy;
    private EnemyMovement enemyMovement;

    private int spawnAmount = 1;
    private float spawnTime;
    private float timer = 0;

    private void Start()
    {
        for (int i = 0; i < startAmount; i++)
        {
            Spawn();
        }
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnTime <= timer) 
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Spawn();
            }
            spawnTime += Random.Range(minSpawnTime, maxSpawnTime);
        }
        if (timer > increaseAmountTime)
        {
            spawnAmount++;
            increaseAmountTime += increaseAmountTime;
        }
    }

    void Spawn()
    {
        enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.PossibleWaypoints = wayPoints;
        enemyMovement.EndPoint = endPoint;
    }
}
