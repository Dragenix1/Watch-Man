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

    private int valueOfCatches;
    public int ValueOfCatches
    { 
        get => valueOfCatches;
        set => valueOfCatches = value;
    }

    private int valueOfEscaped;
    public int ValueOfEscaped
    {
        get => valueOfEscaped;
        set => valueOfEscaped = value;
    }

    [SerializeField]
    private TextMeshProUGUI scoreValue;

    [SerializeField] private int chatchesToWin;
    [SerializeField] private int escapedToLose;

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
        InvokeRepeating(nameof(UpdateScore), 0f, 0.25f);
    }

    private void UpdateScore()
    {
        if(valueOfStolenGoods < 0) valueOfStolenGoods = 0;
        if (valueOfStolenGoods == 0)
        {
            scoreValue.text = "O�";
        }
        else
        {
            scoreValue.text = $"{valueOfStolenGoods - 1},99�";
        }

        if (valueOfCatches >= chatchesToWin)
        {
            //ShowWinScreen
        }
        if(valueOfEscaped >= escapedToLose)
        {
            //ShowLoseScreen
        }
    }
}
