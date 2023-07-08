using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private AudioSource lightTurnOff;
    [SerializeField] private Light light1;
    [SerializeField] private Light light2;
    [SerializeField] private Light light3;
    [SerializeField] private Light light4;
    [SerializeField] private Light light5;
    [SerializeField] private Light light6;
    [SerializeField] private Light light7;
    [SerializeField] private Light light8;
    [SerializeField] private Light light9;

    private int startOrder;


    private void Start()
    {
        optionsMenu.SetActive(false);
    }

    private void Update()
    {
        if (optionsMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenu.SetActive(false);
        }
    }

    public void OnPlayClick()
    {
        StartCoroutine(StartGame());
        //InvokeRepeating("StartStartingCoroutine", 0, 1f);
    }
    public void StartStartingCorourine()
    {
        StartCoroutine(StartGame());
    }

    public void OnOptionClick()
    {
        if (optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(true);
        }
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    private IEnumerator StartGame()
    {
        float time = 1f;

        switch (startOrder)
        {
            case 0:
                startOrder++;
                lightTurnOff.Play();
                StartCoroutine(StartGame());
                break;
            case 1:
                light1.gameObject.SetActive(false);
                light2.gameObject.SetActive(false);
                light3.gameObject.SetActive(false);
                startOrder++;
                yield return new WaitForSeconds(time);
                StartCoroutine(StartGame());
                break;
            case 2:
                light4.gameObject.SetActive(false);
                light5.gameObject.SetActive(false);
                light6.gameObject.SetActive(false);
                startOrder++;
                yield return new WaitForSeconds(time);
                StartCoroutine(StartGame());
                break;
            case 3:
                light7.gameObject.SetActive(false);
                light8.gameObject.SetActive(false);
                light9.gameObject.SetActive(false);
                startOrder++;
                yield return new WaitForSeconds(time);
                StartCoroutine(StartGame());
                break;
            case 4:
                SceneManager.LoadScene(1);
                break;
            default:
            break;
        }
    }
}
