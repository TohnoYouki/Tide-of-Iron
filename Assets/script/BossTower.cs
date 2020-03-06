using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTower : MonoBehaviour {
    public GameObject tower_gameobject;
    public int shelllayer;
    private Rigidbody2D tower;
    public GameObject shell;
    private Vector2 position;
    private float sign_initial;
    private void Start()
    {
        tower = tower_gameobject.GetComponent<Rigidbody2D>();
        position = tower.transform.localPosition;
        pass_time = 0;
        if (sign_set)
            sign_initial = 1.0f;
        else
            sign_initial = -1.0f;
    }

    public float attack_time;
    public float tolattack_time;
    private float pass_time_tol = 0.0f, pass_time = 0.0f;
    public LayerMask enemy_layer;
    private bool if_attack = false;
    private float now_angle = 0.0f;
    public float whole_angle = 360.0f;
    public bool sign_set;
    private float sign;
    private void FixedUpdate()
    {
        TowerTransform();
        if (!if_attack)
            pass_time_tol += Time.deltaTime;
        if (pass_time_tol >= tolattack_time)
        {
            if_attack = true;
            now_angle = 0.0f;
            pass_time_tol = 0.0f;
            sign = sign_initial;
        }

        pass_time += Time.deltaTime;
        if (pass_time >= attack_time)
            pass_time = attack_time;

        if (if_attack)
        {
            if (pass_time>=attack_time)
            {
                pass_time = 0.0f;
                Shoot();
            }
            if (TargetRotate(ref tower, tower_rotate_speed * Time.deltaTime))
                if (sign == sign_initial) 
                {
                    sign = -sign_initial;
                    now_angle = 0.0f;
                }
                else
                    if_attack = false;
        }
    }

    void TowerTransform()
    {
        tower.transform.localPosition = position;
    }

    bool TargetRotate(ref Rigidbody2D source,float angle)
    {
        if (angle < whole_angle - now_angle)
        {
            now_angle += angle;
            source.MoveRotation(source.rotation + sign * angle);
            return false;
        }
        else
        {
            now_angle = whole_angle;
            source.MoveRotation(source.rotation + sign * (whole_angle - now_angle));
            return true;
        }
    }


    public float tower_rotate_speed;

    void Shoot()
    {
        float shellspeed = shell.GetComponent<TankShell>().shellspeed;
        GameObject tank_shell = Instantiate(shell, tower_gameobject.transform) as GameObject;
        tank_shell.layer = shelllayer;
        tank_shell.GetComponent<TankShell>().friendlayer = gameObject.layer;
        tank_shell.transform.parent = null;
        tank_shell.GetComponent<Rigidbody2D>().velocity = tower.transform.up.normalized * shellspeed;
    }
}