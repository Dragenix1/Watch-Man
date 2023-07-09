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

    public GameObject catchText;
    public GameObject dashCam;

    private bool coroutineRunning = false;
    private bool dashcamActive = false;

    public AudioClip[] catchSounds;

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

            AudioClip clip = catchSounds[Random.Range(0, catchSounds.Length)];
            AudioSource.PlayClipAtPoint(clip, transform.parent.transform.position);

            if (!dashcamActive)
                StartCoroutine(ShowDashcam(killTime));
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

    IEnumerator ShowDashcam(float killTime)
    {
        dashcamActive = true;
        dashCam.SetActive(true);
        yield return new WaitForSeconds(killTime);
        dashCam.SetActive(false);
        dashcamActive = false;
        yield return null;
    }
}
