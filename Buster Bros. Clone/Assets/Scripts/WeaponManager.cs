using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum WeaponType
{
    Arrow,
    Plug,
    Shot
}

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    
    private ObjectPool<Projectile> shotPool;
    private ObjectPool<Projectile> arrowPool;
    private ObjectPool<Projectile> plugPool;
    private ObjectPool<WireHitbox> hitboxPool;

    [SerializeField] private Projectile _shotPrefab;
    [SerializeField] private Projectile _arrowPrefab;
    [SerializeField] private Projectile _plugPrefab;
    [SerializeField] private WireHitbox _hitboxPrefab;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        CreatePools();
    }

    void CreatePools()
    {
        shotPool = new ObjectPool<Projectile>(() =>
        {
            return Instantiate(_shotPrefab, transform, true);
        }, shot =>
        {
            shot.gameObject.SetActive(true);
        }, shot =>
        {
            shot.gameObject.SetActive(false);
        }, shot =>
        {
                    
        }, true, 20);
        
        arrowPool = new ObjectPool<Projectile>(() =>
        {
            return Instantiate(_arrowPrefab, transform, true);
        }, arrow =>
        {
            arrow.gameObject.SetActive(true);
        }, arrow =>
        {
            arrow.gameObject.SetActive(false);
        }, arrow =>
        {
                    
        }, true, 20);
        
        plugPool = new ObjectPool<Projectile>(() =>
        {
            return Instantiate(_plugPrefab, transform, true);
        }, plug =>
        {
            plug.gameObject.SetActive(true);
        }, plug =>
        {
            plug.gameObject.SetActive(false);
        }, plug =>
        {
                    
        }, true, 20);
        
        hitboxPool = new ObjectPool<WireHitbox>(() =>
        {
            return Instantiate(_hitboxPrefab, transform, true);
        }, hitbox =>
        {
            hitbox.gameObject.SetActive(true);
        }, hitbox =>
        {
            Debug.Log("HITBOX RETURNED TO POOL");
            hitbox.gameObject.SetActive(false);
        }, hitbox =>
        {
                    
        }, true, 30);
    }

    public void SpawnShot(Transform projectileOriginPos, WeaponType weaponType)
    {
        Projectile newProjectile = null;

        switch (weaponType)
        {
            case WeaponType.Arrow:
                newProjectile = arrowPool.Get();
                break;
            case WeaponType.Shot:
                newProjectile = shotPool.Get();
                break;
            case WeaponType.Plug:
                newProjectile = plugPool.Get();
                break;
        }
        
        newProjectile.transform.position = projectileOriginPos.position;
        newProjectile.Initiate();
    }

    public void ReturnShot(Projectile projectileToReturn)
    {
        switch (projectileToReturn.type)
        {
            case WeaponType.Arrow:
                arrowPool.Release(projectileToReturn);
                break;
            case WeaponType.Shot:
                shotPool.Release(projectileToReturn);
                break;
            case WeaponType.Plug:
                plugPool.Release(projectileToReturn);
                break;
        }
    }

    public void SpawnHitbox(Vector3 spawnPos, Projectile parentProjectile)
    {
        WireHitbox newWire = hitboxPool.Get();

        newWire.transform.position = spawnPos;
        newWire.SetParentProjectile(parentProjectile);
    }
    
    public WireHitbox SpawnHitboxWithReference(Vector3 spawnPos, Projectile parentProjectile)
    {
        WireHitbox newWire = hitboxPool.Get();

        newWire.transform.position = spawnPos;
        newWire.SetParentProjectile(parentProjectile);

        return newWire;
    }

    public void ReturnHitbox(WireHitbox wire)
    {
        hitboxPool.Release(wire);
    }
}
