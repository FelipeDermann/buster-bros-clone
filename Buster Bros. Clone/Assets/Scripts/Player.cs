using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Basic Attributes")]
    [SerializeField] private float moveSpeed;
    
    [Header("Debug Variables ||| DELETE LATER")]
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private bool climbingLadder;

    [Header("References")]
    public PlayerShoot playerShoot;
    public LadderDetector ladderDetector;
    
    InputMap_Main _inputMap;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        _inputMap = new InputMap_Main();
        _inputMap.Enable();
        
        _inputMap.Player.Movement.performed += ctx => GetInput(ctx.ReadValue<Vector2>());
        _inputMap.Player.Movement.canceled += ctx => GetInput(Vector2.zero);

        _inputMap.Player.Fire.performed += ctx => Shoot();
    }

    private void FixedUpdate()
    {
        float horSpeed = moveDir.x * (moveSpeed * 10) * Time.fixedDeltaTime;
        float vertSpeed = moveDir.y * (moveSpeed * 10) * Time.fixedDeltaTime;

        CheckIfClimbingLadder(vertSpeed);
        if (!climbingLadder) vertSpeed = rb.velocity.y; 
        
        Vector2 finalDir = new Vector2(horSpeed, vertSpeed);
        
        rb.velocity = finalDir;
    }

    void GetInput(Vector2 inputDir)
    {
        moveDir = inputDir;
    }

    void CheckIfClimbingLadder(float vertSpeed)
    {
        if (!climbingLadder && vertSpeed != 0 && IsInsideLadder()) ClimbingLadderToggle(true);
        if (climbingLadder && (moveDir.x != 0 || !IsInsideLadder())) ClimbingLadderToggle(false);
    }
    
    void ClimbingLadderToggle(bool state)
    {
        Debug.Log("Climbing ladder");
        climbingLadder = state;
        rb.gravityScale = state ? 0 : 1;
        rb.velocity = state ? new Vector2(0,rb.velocity.y) : new Vector2(rb.velocity.x,0);
        gameObject.layer = state ? 8 : 10;
        if (state) rb.MovePosition(new Vector2(ladderDetector.ladderCollider.transform.position.x, transform.position.y));
    }

    void Shoot()
    {
        playerShoot.Shoot();
    }

    public bool IsInsideLadder()
    {
        return ladderDetector.insideLadder;
    }

    public void Death()
    {
        Debug.Log("Player DEAD!");
    }
}
