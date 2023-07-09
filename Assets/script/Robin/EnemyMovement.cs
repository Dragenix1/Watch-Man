using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float minSpeed = 3.5f;
    [SerializeField] private float maxSpeed = 7.0f;
    private float speed;
    public float Speed { get => speed; }

    [SerializeField] private int minWaypoints = 1;
    [SerializeField] private int maxWaypoints = 4;
    private List<Transform> possibleWaypoints = new List<Transform>();
    public List<Transform> PossibleWaypoints { set { possibleWaypoints = value; } }
    private List<Transform> waypoints = new();
    private int waypointAmount;
    public int WaypointAmount
    {
        get => waypointAmount;
    }

    private Transform endPoint;
    public Transform EndPoint { set { endPoint = value; } }

    private NavMeshAgent agent;

    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioClip footstep1;
    [SerializeField] private AudioClip footstep2;
    private Transform lastTarget;
    private Transform newTarget;
    private bool goalReached = false; //Need to be true when goal reached to determine whether to decrease points or not (reached goal destroy or got caught destroy)
    public bool GoalReached { get => goalReached; }

    private Animator anim;
    private int lootingID;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        lastTarget = possibleWaypoints[Random.Range(0, possibleWaypoints.Count)];
        agent.SetDestination(lastTarget.position);
        agent.speed = Random.Range(minSpeed, maxSpeed);
        anim = GetComponent<Animator>();
        lootingID = Animator.StringToHash("trigLooting");
        waypointAmount = Random.Range(minWaypoints, maxWaypoints + 1);
        for (int i = 0; i < waypointAmount; i++)
        {
            waypoints.Add(possibleWaypoints[Random.Range(0, possibleWaypoints.Count)]);
        }
        waypointAmount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.gameObject.transform == endPoint)
        {
            goalReached = true;
            Destroy(gameObject);
            return;
        }
        if (other.gameObject != lastTarget.gameObject) return;

        possibleWaypoints.Remove(lastTarget);
        if (possibleWaypoints.Count != 0)
        {
            newTarget = possibleWaypoints[Random.Range(0, possibleWaypoints.Count)];
            lastTarget = newTarget;
        }

        if (possibleWaypoints.Count == 0)
        {
            if (lastTarget == endPoint) return;
            lastTarget = endPoint;
        }
        anim.SetTrigger(lootingID);
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

    public void SetNewDestination()
    {
        agent.SetDestination(lastTarget.position);
    }
}
