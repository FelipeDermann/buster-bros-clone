using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShot : Projectile
{
    public override void Death()
    {
        if (!gameObject.activeSelf) return;

        rb.velocity = Vector2.zero;
        WeaponManager.Instance.ReturnShot(this);
    }
}
