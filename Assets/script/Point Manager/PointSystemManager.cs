using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class PointSystemManager : MonoBehaviour
{
    private static PointSystemManager instance;
    public static PointSystemManager Instance
    {
        get => instance;
        set => instance = value;
    }

    private int playerPoints;
    public int PlayerPoints
    {
        get => playerPoints;
        set => playerPoints = value;
    }

    [SerializeField]
    private TextMeshProUGUI scoreValue;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        InvokeRepeating("RepeatCoroutine", 0f, 0.25f);
    }

    private void RepeatCoroutine()
    {
        StartCoroutine(UpdateScore());
    }

    private IEnumerator UpdateScore()
    {
        if(playerPoints < 0) playerPoints = 0;
        scoreValue.text = $"{playerPoints}";
        yield return new WaitForSeconds(0.5f);
    }
}
