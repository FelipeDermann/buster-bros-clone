using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform shotOriginPos;
    
    public void Shoot()
    {
        GameObject bulletPrefab = Instantiate(shotPrefab, shotOriginPos.position, transform.rotation, null);
        Projectile bullet = bulletPrefab.GetComponent<Projectile>();
        bullet.Initiate();
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
