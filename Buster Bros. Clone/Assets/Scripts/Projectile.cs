using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Arrow,
    Plug,
    Shot
}

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initiate()
    {
        Vector2 moveDir = new Vector2(0, projectileSpeed);
        rb.velocity = moveDir;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
