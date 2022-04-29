using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireHitbox : MonoBehaviour
{
    private Projectile _parentProjectile;

    public void SetParentProjectile(Projectile newParentProjectile)
    {
        _parentProjectile = newParentProjectile;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Bubble>().Death();
            _parentProjectile.Death();
        }
    }
}
