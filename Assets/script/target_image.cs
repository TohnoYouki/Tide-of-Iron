using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_image : MonoBehaviour {
    public float life = 1.0f;
    private float disappear_speed;
    private float time;
    private void Start()
    {
        Destroy(this.gameObject, life);
        time = 0.0f;
        disappear_speed = gameObject.GetComponent<RectTransform>().localScale.x / life;
    }
    private void FixedUpdate()
    {
        time = time + Time.deltaTime;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(disappear_speed * (life - time), disappear_speed * (life - time), 0.0f);
    }
}
