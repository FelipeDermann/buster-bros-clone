using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    
    public ObjectPool<Projectile> tier5BubblePool;

    [SerializeField] private Bubble _tier1BubblePrefab;
    
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
    }
    
    
}
