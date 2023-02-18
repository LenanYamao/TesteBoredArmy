using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform playerTransform;
	public float moveSpeed = 5f;
    public bool enemyShooter = false;
    public float distanceToShoot = 4f;
    public float distToPlayer;

	Rigidbody2D rb;
	Animator anim;
    Vector2 movement;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
	}

    private void Update()
    {
        if (!playerTransform) return;
		Vector3 dir = playerTransform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		rb.rotation = angle;
        dir.Normalize();
        movement = dir;
        distToPlayer = Vector2.Distance(playerTransform.position, transform.position);
    }
    private void FixedUpdate()
    {
        if (enemyShooter && distToPlayer <= distanceToShoot) return;
        Move();
    }
    void Move()
    {
        rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));
    }
    public void triggerState(string state)
    {
        if (anim)
        {
            switch (state)
            {
                case "Damaged":
                    anim.SetTrigger("Damaged");
                    break;
                case "Critical":
                    anim.SetTrigger("Critical");
                    break;
                default:
                    break;
            }
        }
    }
}
