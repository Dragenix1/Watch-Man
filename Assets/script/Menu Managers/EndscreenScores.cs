using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndscreenScores : MonoBehaviour
{
    private int robberCatchedValue;
    private int robberEscapedValue;
    private int goodsValue;

    [SerializeField] private TextMeshProUGUI robberCatchedValueText;
    [SerializeField] private TextMeshProUGUI robberEscapedValueText;
    [SerializeField] private TextMeshProUGUI goodValueText;

    public void ShowEndscore(int catched, int escaped, int goods)
    {
        robberCatchedValue = catched;
        robberEscapedValue = escaped;
        goodsValue = goods;

        robberCatchedValueText.text = robberCatchedValue.ToString();
        robberEscapedValueText.text = robberEscapedValue.ToString();
        goodValueText.text = goodsValue.ToString();
    }
}
