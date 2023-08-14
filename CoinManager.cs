using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] Text CoinText;

    public int CoinCount = 0;

    void Start()
    {
        CoinText.GetComponent<Text>().text = "" + CoinCount;
    }

    public void CoinPlus()
    {
        CoinCount += 10;
        CoinText.GetComponent<Text>().text = "" + CoinCount;
    }
}
