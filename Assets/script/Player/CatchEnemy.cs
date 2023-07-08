using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CatchEnemy : MonoBehaviour, IPointBehaviour
{
    private PointSystemManager pointManager;

    private const int basePoints = 50;

    private Animator anim;
    private int catchID;
    private int enemyCatchedID;

    private float killTime = 4.0f;

    public TextMeshProUGUI catchText;

    private bool coroutineRunning = false;

    private void Start()
    {
        pointManager = PointSystemManager.Instance;
        anim = transform.parent.GetComponent<Animator>();
        catchID = Animator.StringToHash("TriggerCatch");
        enemyCatchedID = Animator.StringToHash("isCatched");
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy") && !coroutineRunning)
            StartCoroutine(ShowCatchText());


        if (Input.GetKeyDown(KeyCode.E))
        {

            anim.SetTrigger(catchID);

            other.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            other.gameObject.transform.LookAt(transform.parent.position);
            Animator enemyAnim = other.gameObject.GetComponent<Animator>();
            enemyAnim.SetTrigger(enemyCatchedID);
            Destroy(other.gameObject, killTime);
            int pointsToReceive = (int)(basePoints * other.GetComponent<EnemyMovement>().Speed);
            DecreasePoints(pointsToReceive);
        }
    }

    public void DecreasePoints(int points)
    {
        pointManager.ValueOfStolenGoods -= points;
    }

    public void IncreasePoints(int points)
    {

    }

    IEnumerator ShowCatchText()
    {
        coroutineRunning = true;
        catchText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        catchText.gameObject.SetActive(false);
        coroutineRunning = false;
        yield return null;
    }
}
