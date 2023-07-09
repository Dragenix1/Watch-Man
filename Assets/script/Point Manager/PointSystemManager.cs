using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointSystemManager : MonoBehaviour
{
    private static PointSystemManager instance;
    public static PointSystemManager Instance
    {
        get => instance;
        set => instance = value;
    }

    private int valueOfStolenGoods;
    public int ValueOfStolenGoods
    {
        get => valueOfStolenGoods;
        set => valueOfStolenGoods = value;
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
        if(valueOfStolenGoods < 0) valueOfStolenGoods = 0;
        scoreValue.text = $"{valueOfStolenGoods}€";
        yield return new WaitForSeconds(0.5f);
    }
}
