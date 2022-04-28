using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;
    public float horSpeed;
    public float bounceForce;
    public float direction;
    public int tier;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 horDir = new Vector3(horSpeed * direction, rb.velocity.y);
        rb.velocity = horDir;
    }

    void Death()
    {
        Bubble bubble1 = null;
        Bubble bubble2 = null;
        
        switch (tier)
        {
            case 1:  
                bubble1 = BubblePool.Instance.tier2BubblePool.Get();
                bubble2 = BubblePool.Instance.tier2BubblePool.Get();
                break;
            case 2:  
                bubble1 = BubblePool.Instance.tier3BubblePool.Get();
                bubble2 = BubblePool.Instance.tier3BubblePool.Get();
                break;
            case 3:  
                bubble1 = BubblePool.Instance.tier4BubblePool.Get();
                bubble2 = BubblePool.Instance.tier4BubblePool.Get();
                break;
            case 4:  
                bubble1 = BubblePool.Instance.tier5BubblePool.Get();
                bubble2 = BubblePool.Instance.tier5BubblePool.Get();
                break;
            case 5:
                gameObject.SetActive(false);
                return;
        }
        
        bubble1.transform.position = transform.position;
        bubble1.ChangeDirection(-1);
        bubble1.rb.AddForce(Vector2.up * bounceForce/2, ForceMode2D.Impulse);
        
        bubble2.transform.position = transform.position;
        bubble2.ChangeDirection(1);
        bubble2.rb.AddForce(Vector2.up * bounceForce/2, ForceMode2D.Impulse);

        gameObject.SetActive(false);
    }

    void ChangeDirection(float newDirection)
    {
        direction = newDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 bounceDir = collision.GetContact(0).normal;
        Debug.Log("CONTACT COUNT: " + collision.contactCount);
        if (bounceDir.x >= 1 || bounceDir.x <= -1) ChangeDirection(Mathf.Sign(bounceDir.x));

        if (bounceDir.y >= 0.5f || bounceDir.y <= -0.5f)
        {
            bounceDir.x = 0;
            rb.velocity = Vector2.zero;
            Vector2 forceToApply = bounceDir * bounceForce;
            
            if (bounceDir.y < 0) forceToApply /= 2;
            rb.AddForce(forceToApply, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Death();
        }
        
        if (other.CompareTag("Projectile"))
        {
            other.GetComponent<Projectile>().Death();
            Death();
        }
    }
}
