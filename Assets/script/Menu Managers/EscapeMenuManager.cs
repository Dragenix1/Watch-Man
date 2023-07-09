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
    [SerializeField]
    private GameObject endScreen;

    [SerializeField]
    private GameObject winText;
    [SerializeField]
    private GameObject loseText;

    private const float normalGameTime = 1.0f;
    private const float stoppedGameTime = 0f;

    [SerializeField]
    private EndscreenScores endscreenScores;


    private void Start()
    {
        endscreenScores = gameObject.GetComponent<EndscreenScores>();   

        escapeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        endScreen.SetActive(false);
        winText.SetActive(false);
        loseText.SetActive(false);


        PointSystemManager.Instance.menuManager = this;
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
            else if (!endScreen.activeInHierarchy)
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
        Time.timeScale = normalGameTime;
        SceneManager.LoadScene(0);
    }

    public void OnQuitGameClick()
    {
        Application.Quit();
    }

    public void ShowWinScreen()
    {
        winText.SetActive(true );
        loseText.SetActive(false);

        Time.timeScale = stoppedGameTime;
        endScreen.SetActive(true);
        endscreenScores.ShowEndscore(PointSystemManager.Instance.ValueOfCatches, PointSystemManager.Instance.ValueOfEscaped, PointSystemManager.Instance.ValueOfStolenGoods);
    }

    public void ShowLoseScreen()
    {
        winText.SetActive(false);
        loseText.SetActive(true);

        Time.timeScale = stoppedGameTime;
        endScreen.SetActive(true);
        endscreenScores.ShowEndscore(PointSystemManager.Instance.ValueOfCatches, PointSystemManager.Instance.ValueOfEscaped, PointSystemManager.Instance.ValueOfStolenGoods);
    }
}
