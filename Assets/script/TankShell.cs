using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShell : MonoBehaviour {
    
    public float shellspeed;
    public float shellpower;
    public float lifetime;
    public int friendlayer;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer != friendlayer) && (collision.gameObject.tag!="shell"))
        {
            Health health = collision.GetComponent<Health>();
            if (health)
                health.GetDamage(shellpower);
            Destroy(gameObject);
        }
    }
}
