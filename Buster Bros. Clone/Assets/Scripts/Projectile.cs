using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action CanShootAgain;
    
    public WeaponType type;
    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }
    
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
    
    public virtual void Initiate(float horSpeed)
    {
        Vector2 moveDir = new Vector2(horSpeed, projectileSpeed);
        rb.velocity = moveDir;
    }

    public virtual void Death()
    {
        if (type != WeaponType.Shot) CanShootAgain?.Invoke();
    }

    public virtual void GroundInteraction()
    {
        Death();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GroundInteraction();
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
