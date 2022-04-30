using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Attributes")]
    [SerializeField] private float moveSpeed;
    
    [Header("Debug Variables ||| DELETE LATER")]
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private bool climbingLadder;
    
    [Header("References")]
    public LadderDetector ladderDetector;

    private InputMap_Main _inputMap;
    private Rigidbody2D rb;
    
    public void Initiate(InputMap_Main inputMap)
    {
        rb = GetComponent<Rigidbody2D>();
        
        _inputMap = inputMap;
        _inputMap.Player.Movement.performed += ctx => GetInput(ctx.ReadValue<Vector2>());
        _inputMap.Player.Movement.canceled += ctx => GetInput(Vector2.zero);
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
    
    void CheckIfClimbingLadder(float vertSpeed)
    {
        if (!climbingLadder && vertSpeed != 0 && IsInsideLadder()) ClimbingLadderToggle(true);
        if (climbingLadder && (moveDir.x != 0 || !IsInsideLadder())) ClimbingLadderToggle(false);
    }
    
    void ClimbingLadderToggle(bool state)
    {
        climbingLadder = state;
        rb.gravityScale = state ? 0 : 1;
        rb.velocity = state ? new Vector2(0,rb.velocity.y) : new Vector2(rb.velocity.x,0);
        gameObject.layer = state ? 8 : 10;
        if (state) rb.MovePosition(new Vector2(ladderDetector.ladderCollider.transform.position.x, transform.position.y));
    }
    
    void GetInput(Vector2 inputDir)
    {
        moveDir = inputDir;
    }
    
    public bool IsInsideLadder()
    {
        return ladderDetector.insideLadder;
    }

    public void DeathAnim()
    {
        rb.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque(60);
    }
}
