using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unite : MonoBehaviour {
    public LayerMask friend;
    private Collider2D[] friend_target;
    public float distance;
    public float armor_per_unit;
    private float initial_armor;
    private void Start()
    {
        initial_armor = gameObject.GetComponent<Health>().armor;
    }
    private void Update()
    {
        int count;
        friend_target = Physics2D.OverlapCircleAll(gameObject.transform.position, distance, friend);
        count = friend_target.Length;
        gameObject.GetComponent<Health>().ChangeArmor(initial_armor + count * armor_per_unit);
    }
}
