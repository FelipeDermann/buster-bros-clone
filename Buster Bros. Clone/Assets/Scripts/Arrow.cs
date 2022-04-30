using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private float _hitboxGrowRate;
    private Coroutine _hitboxExpandCoroutine;
    [SerializeField] private GameObject wireHitbox;

    public override void Awake()
    {
        base.Awake();
        wireHitbox.GetComponentInChildren<WireHitbox>().SetParentProjectile(this);
    }
    
    public override void Initiate()
    {
        base.Initiate();
        SpawnInitialHitbox();
    }

    public void SpawnInitialHitbox()
    {
        wireHitbox.transform.position = transform.position + new Vector3(0,-1,0);
        wireHitbox.transform.SetParent(null);
        wireHitbox.SetActive(true);
        
        _hitboxExpandCoroutine = StartCoroutine(ExpandHitbox());
    }

    IEnumerator ExpandHitbox()
    {
        while(true)
        {
            wireHitbox.transform.localScale += new Vector3(0, _hitboxGrowRate * Time.deltaTime, 0);
            //yield return new WaitForSeconds(_delayOnGrowths);
            yield return null;
        }
    }
    
    public override void Death()
    {
        if (!gameObject.activeSelf) return;
        base.Death();
        
        rb.velocity = Vector2.zero;
        WeaponManager.Instance.ReturnShot(this);
        Debug.Log("DESTROY ARROW");

        if(_hitboxExpandCoroutine != null) StopCoroutine(_hitboxExpandCoroutine);
        wireHitbox.SetActive(false);
        wireHitbox.transform.SetParent(transform);
        wireHitbox.transform.localScale = new Vector3(1, 1, 1);
    }
}
