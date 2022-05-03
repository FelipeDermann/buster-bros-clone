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

    [SerializeField] private Projectile _shotPrefab;
    [SerializeField] private Projectile _arrowPrefab;
    [SerializeField] private Projectile _plugPrefab;
    
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
    }

    public void SpawnShot(Transform projectileOriginPos, WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Arrow:
                ArrowShot(projectileOriginPos);
                break;
            case WeaponType.Shot:
                GatlingShot(projectileOriginPos);
                break;
            case WeaponType.Plug:
                PlugShot(projectileOriginPos);
                break;
        }
    }

    void ArrowShot(Transform projectileOriginPos)
    {
        Projectile newProjectile = arrowPool.Get();
        newProjectile.transform.position = projectileOriginPos.position;
        newProjectile.Initiate();
    }
    
    void PlugShot(Transform projectileOriginPos)
    {
        Projectile newProjectile = plugPool.Get();
        newProjectile.transform.position = projectileOriginPos.position;
        newProjectile.Initiate();
    }

    void GatlingShot(Transform projectileOriginPos)
    {
        Vector3 closeSpawnPos = projectileOriginPos.position + new Vector3(0.35f,0,0);
        for (int i = 0; i < 2; i++)
        {
            Projectile newProjectile = shotPool.Get();
            
            newProjectile.transform.position = closeSpawnPos;
            newProjectile.Initiate();
            
            closeSpawnPos = projectileOriginPos.position - new Vector3(0.35f,0,0);
        }
        
        Vector3 farSpawnPos = projectileOriginPos.position + new Vector3(1,0,0);
        for (int i = 0; i < 2; i++)
        {
            Projectile newProjectile = shotPool.Get();
            float horSpeed = newProjectile.ProjectileSpeed * 0.1f;
            float finalHorSpeed = i == 0 ? horSpeed: -horSpeed;
            
            newProjectile.transform.position = farSpawnPos;
            newProjectile.Initiate(finalHorSpeed);
            
            farSpawnPos = projectileOriginPos.position - new Vector3(1,0,0);
        }
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
}
