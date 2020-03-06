using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {
    public management select_management;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void FixedUpdate()
    {
        text.text = "Now Money: " + select_management.GetMoney().ToString() + "$";
    }
}
