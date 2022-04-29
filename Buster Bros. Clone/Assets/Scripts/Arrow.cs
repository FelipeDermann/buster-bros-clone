using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private float _hitboxGrowRate;
    private Coroutine _spawnerCoroutine;
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
        
        _spawnerCoroutine = StartCoroutine(ExpandHitbox());
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
        
        rb.velocity = Vector2.zero;
        WeaponManager.Instance.ReturnShot(this);
        Debug.Log("DESTROY ARROW");

        StopCoroutine(_spawnerCoroutine);
        wireHitbox.SetActive(false);
        wireHitbox.transform.localScale = new Vector3(1, 1, 1);
    }
}
