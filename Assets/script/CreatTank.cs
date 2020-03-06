using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatTank : MonoBehaviour {
    public GameObject tank;
    public Transform create_position;
    public float cooldown_time;
    public float crazy_time;
    private float time, time2;
    public int initial_number;
    public int cooldown_number;
    public int crazy_number;
    private void Start()
    {
        time = 0;
        time2 = 0;
        for (int i = 0; i < initial_number; i++) 
            Instantiate(tank, create_position);
    }
    private void FixedUpdate()
    {
        time = time + Time.deltaTime;
        time2 = time2 + Time.deltaTime;
        if (time >= cooldown_time)
        {
            create(cooldown_number);
            time = 0.0f;
        }
        if (time2 > crazy_time)
        {
            create(crazy_number);
            time2 = 0.0f;
        }
    }
    public void create(int number)
    {
        for (int i = 0; i < number; i++)
            Instantiate(tank, create_position);
    }
}
