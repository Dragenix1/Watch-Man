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
    [SerializeField] private Transform endPoint;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioClip footstep1;
    [SerializeField] private AudioClip footstep2;
    private Transform lastTarget;
    private Transform newTarget;
    private LevelGenerator levelGenerator;
    private bool goalReached = false; //Need to be true when goal reached to determine whether to decrease points or not (reached goal destroy or got caught destroy)
    public bool GoalReached { get => goalReached; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        levelGenerator = LevelGenerator.Instance;
        targets = levelGenerator.Targets;
        endPoint = levelGenerator.EndPoint;
        lastTarget = targets[Random.Range(0, targets.Count - 1)];
        agent.SetDestination(lastTarget.position);
        agent.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
       if(other.gameObject.transform == endPoint)
        {
            goalReached = true;
            Destroy(gameObject);
            return;
        }
        if (other.gameObject != lastTarget.gameObject) return;

        if (targets.Count != 0)
        {
            targets.Remove(lastTarget); 
        }
        if(targets.Count != 0)
        {
            newTarget = targets[Random.Range(0, targets.Count - 1)];
        }

        lastTarget = newTarget;
        if(targets.Count == 0)
        {
            if (lastTarget == endPoint) return;
            lastTarget = endPoint;
        }
        agent.SetDestination(lastTarget.position);
    }

    public void PlayFoot1()
    {
        footstep.clip = footstep1;
        footstep.Play();
    }

    public void PlayFoot2()
    {
        footstep.clip = footstep2;
        footstep.Play();
    }
}
