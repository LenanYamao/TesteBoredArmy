using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform cannonFront;
    public List<Transform> cannonsRight;
    public List<Transform> cannonsLeft;
    public GameObject bullet;
    public float shotSpeed = 5f;
    public float delayBetweenShots = 1f;

    private bool _tiroFrontal = false;
    private bool _tiroLateral= false;
    private bool _canShoot = true;
    void Update()
    {
        GetShooting();

        Shoot();
    }
    private void GetShooting()
    {
        _tiroFrontal = Input.GetButtonDown("TiroFrontal");
        _tiroLateral = Input.GetButtonDown("TiroLateral");
    }
    private void Shoot()
    {
        if (_tiroFrontal)
        {
            AttackFront();
        }
        if (_tiroLateral)
        {
            AttackSides();
        }
    }

    public void AttackFront()
    {
        if (!_canShoot) return;
        _canShoot = false;

        GameObject bulletInstance = Instantiate(bullet, cannonFront.position, cannonFront.rotation);
        var bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(cannonFront.up * shotSpeed);
        Destroy(bulletInstance, 3f);

        StartCoroutine(ShootDelay());
    }
    public void AttackSides()
    {
        if (!_canShoot) return;
        _canShoot = false;

        foreach(var cannon in cannonsRight)
        {
            GameObject bulletInstance = Instantiate(bullet, cannon.position, cannon.rotation);
            var bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(cannon.right * shotSpeed);
            Destroy(bulletInstance, 3f);
        }
        foreach (var cannon in cannonsLeft)
        {
            GameObject bulletInstance = Instantiate(bullet, cannon.position, cannon.rotation);
            var bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(cannon.right * shotSpeed * -1);
            Destroy(bulletInstance, 3f);
        }

        StartCoroutine(ShootDelay());
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayBetweenShots);
        _canShoot = true;
    }
}
