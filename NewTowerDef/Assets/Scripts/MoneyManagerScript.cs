using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagerScript : MonoBehaviour
{
    public static MoneyManagerScript obj;
    public Text textField;
    public int CoinOfLuck;
    void Awake()
    {
        obj = this;
    }

    void Update()
    {
        textField.text = CoinOfLuck.ToString();
    }
}
