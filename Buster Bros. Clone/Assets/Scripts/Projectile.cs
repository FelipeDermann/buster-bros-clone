using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public WeaponType type;
    public float projectileSpeed;
    protected Rigidbody2D rb;
    
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Initiate()
    {
        Vector2 moveDir = new Vector2(0, projectileSpeed);
        rb.velocity = moveDir;
    }

    public virtual void Death()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Death();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Bubble>().Death();
            Death();
        }
    }
}
