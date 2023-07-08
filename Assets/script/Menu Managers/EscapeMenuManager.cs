using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject escapeMenu;
    [SerializeField]
    private GameObject optionsMenu;

    private const float normalGameTime = 1.0f;
    private const float stoppedGameTime = 0f;

    private void Start()
    {
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(false);
                escapeMenu.SetActive(true);
            }
            else if(escapeMenu.activeInHierarchy)
            {
                Time.timeScale = normalGameTime;
                escapeMenu.SetActive(false);
                optionsMenu.SetActive(false);
            }
            else
            {
                escapeMenu.SetActive(true);
                Time.timeScale = stoppedGameTime;
            }
        }
    }
    
    public void OnContinueClick()
    {
        if (escapeMenu.activeInHierarchy)
        {
            Time.timeScale = normalGameTime;
            escapeMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
        else
        {
            escapeMenu.SetActive(true);
            Time.timeScale = stoppedGameTime;
        }
    }

    public void OnOptionClick()
    {
        if(optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);
            escapeMenu.SetActive(true);
        }
        else
        {
            optionsMenu.SetActive(true);
            escapeMenu.SetActive(false); 
        }
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitGameClick()
    {
        Application.Quit();
    }
}
