using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public int damage = 1;
    DamageableEntity damageable;
    private void Start()
    {
        damageable = GetComponent<DamageableEntity>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var damageableScript = col.gameObject.GetComponent<DamageableEntity>();

            if (damageableScript != null)
            {
                damageableScript.TakeDamage(damage);
            }
            damageable.selfDestruct();
        }
    }
}
