using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class management : MonoBehaviour {
    public int initial_money = 1000;
    private int money;
    private void Start()
    {
        money = initial_money;
    }
    public bool AddMoney(int amount)
    {
        money += amount;
        return true;
    }
    public bool DecreaseMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        else
            return false;
    }
    public int GetMoney()
    {
        return money;
    }
}
