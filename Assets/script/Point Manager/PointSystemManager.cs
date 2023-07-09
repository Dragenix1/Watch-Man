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
    [SerializeField]
    private TextMeshProUGUI escapedValue; 
    [SerializeField]
    private TextMeshProUGUI catchValue;

    [SerializeField] private int chatchesToWin;
    [SerializeField] private int escapedToLose = 1;

    public EscapeMenuManager menuManager;

    public AudioClip cash;
    public Animation textAnim;

    private void Awake()
    {
        if (instance != null && instance != this)
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
        if (valueOfStolenGoods < 0) valueOfStolenGoods = 0;
        scoreValue.text = $"{valueOfStolenGoods}€";
        escapedValue.text = $"{valueOfEscaped}/{escapedToLose}";
        catchValue.text = $"{valueOfCatches}/{chatchesToWin}";
        if (valueOfCatches >= chatchesToWin)
        {
            //ShowWinScreen
            menuManager.ShowWinScreen();
        }
        if (valueOfEscaped >= escapedToLose)
        {
            //ShowLoseScreen
            menuManager.ShowLoseScreen();
        }
    }
}
