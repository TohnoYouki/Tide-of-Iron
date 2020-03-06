using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour {
    public Transform create_position;
    public GameObject creation;
    public management select_management;
    public int money;
    public float cooldown_time;
    private float time;
    private Slider slider;
    private void Start()
    {
        time = cooldown_time;
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = 1.0f;
    }
    private void FixedUpdate()
    {
        time = time + Time.deltaTime;
        if (time >= cooldown_time)
            time = cooldown_time;
        slider.value = time / cooldown_time;
    }
    public void create()
    {
        if ((time >= cooldown_time) && (select_management.DecreaseMoney(money)))
        {
            Instantiate(creation, create_position);
            time = 0.0f;
        }
    }
}
