using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private WeaponType currentWeaponType;
    [SerializeField] private Transform shotOriginPos;

    public WeaponType CurrentWeaponType { get{ return currentWeaponType; }}

    public void Shoot()
    {
        WeaponManager.Instance.SpawnShot(shotOriginPos, currentWeaponType);
    }

    public void ChangeWeaponType(WeaponType newWeapon)
    {
        currentWeaponType = newWeapon;
    }
}
