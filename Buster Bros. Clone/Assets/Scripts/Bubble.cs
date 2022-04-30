using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left,
    None
}

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;
    public float horSpeed;
    public float bounceForce;
    public Direction direction;
    public int tier;

    private Collider2D myCollider;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        
        ChangeDirection(direction);
    }

    private void FixedUpdate()
    {
        float floatDir = direction == Direction.Right ? 1 : -1;
        if (direction == Direction.None) floatDir = 0;
        
        Vector3 horDir = new Vector3(horSpeed * floatDir, rb.velocity.y);
        rb.velocity = horDir;
    }

    public void Death()
    {
        if (!gameObject.activeSelf) return;
        
        BubbleManager.Instance.DecreaseBubble();

        Bubble bubble1 = null;
        Bubble bubble2 = null;
        
        switch (tier)
        {
            case 1:  
                bubble1 = BubbleManager.Instance.tier2BubblePool.Get();
                bubble2 = BubbleManager.Instance.tier2BubblePool.Get();
                break;
            case 2:  
                bubble1 = BubbleManager.Instance.tier3BubblePool.Get();
                bubble2 = BubbleManager.Instance.tier3BubblePool.Get();
                break;
            case 3:  
                bubble1 = BubbleManager.Instance.tier4BubblePool.Get();
                bubble2 = BubbleManager.Instance.tier4BubblePool.Get();
                break;
            case 4:  
                bubble1 = BubbleManager.Instance.tier5BubblePool.Get();
                bubble2 = BubbleManager.Instance.tier5BubblePool.Get();
                break;
            case 5:
                gameObject.SetActive(false);
                return;
        }
        
        bubble1.transform.position = transform.position;
        bubble1.ChangeDirection(Direction.Left);
        bubble1.rb.AddForce(Vector2.up * bounceForce/2, ForceMode2D.Impulse);
        
        bubble2.transform.position = transform.position;
        bubble2.ChangeDirection(Direction.Right);
        bubble2.rb.AddForce(Vector2.up * bounceForce/2, ForceMode2D.Impulse);

        gameObject.SetActive(false);
    }

    void ChangeDirection(Direction newDirection)
    {
        direction = newDirection;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Physics2D.IgnoreCollision(myCollider, other.collider, true);

        for (int i = 0; i < other.contactCount; i++)
        {
            Vector2 bounceDir = other.GetContact(i).normal;
            if (bounceDir.x >= 1 || bounceDir.x <= -1)
            {
                float dirValue = Mathf.Sign(bounceDir.x);
                Direction dir = dirValue >= 1 ? Direction.Right : Direction.Left;
                ChangeDirection(dir);
            }

            if (bounceDir.y >= 0.5f || bounceDir.y <= -0.5f)
            {
                bounceDir.x = 0;
                rb.velocity = Vector2.zero;
                Vector2 forceToApply = bounceDir * bounceForce;

                if (bounceDir.y < 0) forceToApply /= 2;
                rb.AddForce(forceToApply, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Physics2D.IgnoreCollision(myCollider, other.collider, false);
    }
}
