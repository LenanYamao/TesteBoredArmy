using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger:MonoBehaviour
{
	public int damage = 5;
    public bool enemyShot = false;
    public GameObject explosionFx;
    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Enemy") && !enemyShot)
		{
            var damageableScript = col.GetComponent<DamageableEntity>();

            if (damageableScript != null)
            {
                damageableScript.TakeDamage(damage);
            }
            if (explosionFx != null)
            {
                GameObject fx = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(fx, .5f);
            }
            Destroy(gameObject);
        }
        else if (col.CompareTag("Player") && enemyShot)
		{
            var damageableScript = col.GetComponent<DamageableEntity>();

            if (damageableScript != null)
            {
                damageableScript.TakeDamage(damage);
            }
            if (explosionFx != null)
            {
                GameObject fx = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(fx, .5f);
            }
            Destroy(gameObject);
        }
    }
}
