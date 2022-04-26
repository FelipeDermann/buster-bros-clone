using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetector : MonoBehaviour
{
    public LayerMask layerMask;
    public bool insideLadder;
    public Collider2D ladderCollider;
    
    private void FixedUpdate()
    {
        CheckLadderCollision();
    }

    void CheckLadderCollision()
    {
        ladderCollider = Physics2D.OverlapBox(transform.position, transform.localScale,0, layerMask);
        if (ladderCollider != null) insideLadder = true;
        else insideLadder = false;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
