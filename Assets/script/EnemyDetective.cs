using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetective : MonoBehaviour {
    public LayerMask enemy;
    private Collider2D[] enemy_target;
    public float distance;
    private float initial_attack_time;
    private Attack_Add attack_add;
    private float attack_time;
    private void Start()
    {
        initial_attack_time = gameObject.GetComponent<Tower>().attack_time;
    }
    private void Update()
    {
        attack_time = 0.0f;
        enemy_target = Physics2D.OverlapCircleAll(gameObject.transform.position, distance, enemy);
        for (int i = 0; i < enemy_target.Length; i++) 
        {
            attack_add = enemy_target[i].GetComponent<Attack_Add>();
            if (attack_add)
                attack_time += attack_add.attack_time;
        }
        attack_time += initial_attack_time;
        gameObject.GetComponent<Tower>().attack_time = attack_time;
    }
}
