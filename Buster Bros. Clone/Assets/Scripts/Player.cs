using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    InputMap_Main _inputMap;
    [Header("References")]
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Collider2D collider;
    [SerializeField] private Collider2D trigger;

    private bool _dead;

    private void Awake()
    {
        _inputMap = new InputMap_Main();
        _inputMap.Enable();
        _inputMap.Player.Fire.performed += ctx => Shoot();
        
        playerMovement.Initiate(_inputMap);
        GameManager.LevelOver += DisableInputs;
    }

    private void OnDestroy()
    {
        GameManager.LevelOver -= DisableInputs;
    }

    void Shoot()
    {
        playerShoot.Shoot();
    }

    public void Death()
    {
        collider.enabled = false;
        trigger.enabled = false;

        DisableInputs();
        playerMovement.DeathAnim();
        GameManager.Instance.PlayerDefeat();
    }

    void DisableInputs()
    {
        _inputMap.Disable();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_dead) return;
            
            _dead = true;
            Death();
        }
    }
}
