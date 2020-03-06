using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treat : MonoBehaviour {
    public float min_treat_distance;
    public float max_treat_distance;
    public float treat_distance;
    public float treat_number;
    public float treat_time;
    public LayerMask friend;
    public LayerMask enemy;
    private Collider2D[] friend_target;
    private Collider2D[] enemy_target;
    Health health;
    private float time = 0.0f;
    private float tol_damage = 0.0f;
    public float max_damage = 500.0f;
    public int number;
    private void Update()
    {
        int j;
        j = 0;
        if (tol_damage >= max_damage)
            tol_damage = max_damage;
        treat_distance = Mathf.Lerp(min_treat_distance, max_treat_distance, tol_damage / max_damage);
        time = time + Time.deltaTime;
        if (time >= treat_time)
        {
            friend_target = Physics2D.OverlapCircleAll(gameObject.transform.position, treat_distance, friend);
            enemy_target = Physics2D.OverlapCircleAll(gameObject.transform.position, treat_distance, enemy);
            for (int i = 0; i < friend_target.Length; i++)
            {
                health = friend_target[i].GetComponent<Health>();
                if (health)
                    if (tol_damage > 0)
                    {
                        tol_damage = tol_damage + health.GetDamage(-treat_number) * 3;
                        if (tol_damage < 0)
                            tol_damage = 0;
                    }
            }
            for (int i = 0; i < enemy_target.Length; i++)
            {
                health = enemy_target[i].GetComponent<Health>();
                if ((health) && (j < number)) 
                {
                    tol_damage = tol_damage + health.GetDamage(treat_number);
                    j++;
                }
            }
            time = 0.0f;
        }
    }
}
