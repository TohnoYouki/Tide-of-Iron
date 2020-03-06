using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public GameObject tower_gameobject;
    public GameObject target_gameobject;
    public GameObject attack_gameobject;
    public int shelllayer;
    private Rigidbody2D tower;
    public GameObject shell;
    public bool select;
    public float attack_distance;
    public float min_attack_angle;
    public float min_attack_distance_error;
    private Vector2 position;
    private void OnEnable()
    {
        tower = tower_gameobject.GetComponent<Rigidbody2D>();
        position = tower.transform.localPosition;
    }
    private void Start()
    {
        target_gameobject = null;
        attack_gameobject = null;
        if_attack = false;
        pass_time = 0;
        distance = attack_distance + min_attack_distance_error;
        distance = distance * distance;
    }

    public float attack_time;
    private float pass_time;
    public LayerMask enemy_layer;
    private Collider2D attack_collider;
    private void FixedUpdate()
    {
        if (attack_gameobject)
            adistance = (attack_gameobject.transform.position - tower.transform.position).sqrMagnitude;
        if (!(attack_gameobject && adistance <= distance))
        {
            attack_collider = Physics2D.OverlapCircle(transform.position, attack_distance, enemy_layer);
            if (!attack_collider)
                attack_gameobject = null;
            else
                attack_gameobject = attack_collider.gameObject;
        }
        if (target_gameobject)
            tdistance = (target_gameobject.transform.position - tower.transform.position).sqrMagnitude;
        TowerTransform();
        Attack();
    }

    private bool if_attack;
    void TowerTransform()
    {
        select = GetComponentInParent<Health>().select;
        tower.transform.localPosition = position;
        if (!target_gameobject && !attack_gameobject && select)
            TowerMouseRotate();
        if ((attack_gameobject && attack_distance<=distance) && (!(target_gameobject && tdistance<=distance)))
            TargetRotate(ref tower, attack_gameobject.transform.position, tower_rotate_speed);
        else if (target_gameobject)
            TargetRotate(ref tower, target_gameobject.transform.position, tower_rotate_speed);
    }

    private float tdistance, adistance, distance;
    private bool select_fire;
    void Attack()
    {
        if (gameObject.GetComponent<ClickToMove>())
            select_fire = gameObject.GetComponent<ClickToMove>().select;
        else
            select_fire = false;
        pass_time += Time.deltaTime;
        if (target_gameobject && tdistance <= distance)
            if_attack = true;
        else if (attack_gameobject && attack_distance <= distance)
            if_attack = true;
        else if (!target_gameobject && Input.GetButtonDown("Jump") && select_fire)
            if_attack = true;
        else
            if_attack = false;
        if (pass_time > attack_time)
            if (if_attack)
            {
                Shoot();
                pass_time = 0.0f;
            }
            else
                pass_time = attack_time;
    }

    bool TargetRotate(ref Rigidbody2D source, Vector2 target, float rotatespeed)
    {
        float angle;
        Vector2 dirction = target - source.position;
        Vector2 up = source.transform.up;
        if ((dirction.x == 0) && (dirction.y == 0))
            angle = 0.0f;
        else
            angle = Vector2.Angle(up, dirction);
        float sign;
        if (up.x * dirction.y > dirction.x * up.y)
            sign = -1;
        else
            sign = 1;
        angle = -sign * Mathf.Min(Time.deltaTime * rotatespeed, angle);
        source.MoveRotation(source.rotation + angle);
        if (angle <= min_attack_angle)
            return true;
        else
            return false;
    }

    Vector2 MousePosition()
    {
        Vector2 position = Input.mousePosition;
        position = Camera.main.ScreenToWorldPoint(position);
        return position;
    }

    public float tower_rotate_speed;
    void TowerMouseRotate()
    {
        Vector2 mouseposition = MousePosition();
        TargetRotate(ref tower, mouseposition, tower_rotate_speed);
    }

    void Shoot()
    {
        float shellspeed = shell.GetComponent<TankShell>().shellspeed;
        if (tower.GetComponent<AudioSource>())
            tower.GetComponent<AudioSource>().Play();
        GameObject tank_shell = Instantiate(shell, tower_gameobject.transform) as GameObject;
        tank_shell.layer = shelllayer;
        tank_shell.GetComponent<TankShell>().friendlayer = gameObject.layer;
        tank_shell.transform.parent = null;
        tank_shell.GetComponent<Rigidbody2D>().velocity = tower.transform.up.normalized * shellspeed;
    }
}
