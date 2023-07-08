using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;

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
        SceneManager.LoadScene(1);
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
}
