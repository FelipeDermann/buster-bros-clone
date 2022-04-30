using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private WeaponType currentWeaponType;
    [SerializeField] private Transform shotOriginPos;
    [SerializeField] private bool canShoot = true;

    public WeaponType CurrentWeaponType { get{ return currentWeaponType; }}

    private void Awake()
    {
        Projectile.CanShootAgain += CanShootAgain;
    }

    private void OnDestroy()
    {
        Projectile.CanShootAgain -= CanShootAgain;
    }

    public void Shoot()
    {
        if (!canShoot) return;
        
        WeaponManager.Instance.SpawnShot(shotOriginPos, currentWeaponType);
        if (currentWeaponType != WeaponType.Shot) canShoot = false;
    }

    void CanShootAgain()
    {
        canShoot = true;
    }
    
    public void ChangeWeaponType(WeaponType newWeapon)
    {
        currentWeaponType = newWeapon;
    }
}
