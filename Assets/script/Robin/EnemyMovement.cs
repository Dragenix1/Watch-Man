using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.5f;
    public float Speed { get => speed; }
    private NavMeshAgent agent;
    private Rigidbody rb;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    private Transform lastTarget;
    private Transform newTarget;
    private bool goalReached = false; //Need to be true when goal reached to determine whether to decrease points or not (reached goal destroy or got caught destroy)
    public bool GoalReached { get => goalReached; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        lastTarget = targets[Random.Range(0, targets.Count - 1)];
        agent.SetDestination(lastTarget.position);
        agent.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != lastTarget.gameObject) return;

        do
        {
            newTarget = targets[Random.Range(0, targets.Count - 1)];
        } while (newTarget == lastTarget);

        lastTarget = newTarget;
        agent.SetDestination(lastTarget.position);
    }
}
