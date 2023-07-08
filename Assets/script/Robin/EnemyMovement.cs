using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.5f;
    public float Speed { get => speed; }
    private NavMeshAgent agent;
    private Rigidbody rb;
    [SerializeField] private Transform[] targets;
    private Transform lastTarget;
    private Transform newTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        lastTarget = targets[Random.Range(0, targets.Length - 1)];
        agent.SetDestination(lastTarget.position);
        agent.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != lastTarget.gameObject) return;

        do
        {
            newTarget = targets[Random.Range(0, targets.Length - 1)];
        } while (newTarget == lastTarget);

        lastTarget = newTarget;
        agent.SetDestination(lastTarget.position);
    }
}
