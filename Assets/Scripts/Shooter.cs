using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform cannonPosition;
    public GameObject cannonball;
    public float shotSpeed = 5f;
    public float delayBetweenShots = 5f;

    bool _canShoot = true;
    Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.distToPlayer == 0) return;
        if (enemy.distToPlayer <= enemy.distanceToShoot)
        {
            if (!_canShoot) return;
            _canShoot = false;

            GameObject bulletInstance = Instantiate(cannonball, cannonPosition.position, cannonPosition.rotation);
            var bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(cannonPosition.right * shotSpeed);
            Destroy(bulletInstance, 3f);

            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayBetweenShots);
        _canShoot = true;
    }
}
