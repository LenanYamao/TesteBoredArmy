using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity:MonoBehaviour
{
	public GameManager gm;
	public int HP = 5;
	public int MaxHP = 5;
	public bool hasIframes = false;
	public float iframeDuration = 0.5f;
	public bool canTakeDamage = true;
	public bool blinkOnDamage = false;
	public bool camShakeOnDamage = false;
	public bool isEnemy = true;
	public bool isPlayer = false;
	public string levelName = "";
	public GameObject explosionFx;
	public Transform fxPoint;
	public PlayerController pc;
	public Enemy enemy;

	float spriteBlinkingTimer = 0.0f;
	float spriteBlinkingMiniDuration = 0.1f;
	float spriteBlinkingTotalTimer = 0.0f;

	bool blinking = false;
	bool endGame = false;

	Rigidbody2D rb;
	SpriteRenderer sprite;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	void Update()
	{
		if (HP <= 0)
		{
			if (isPlayer && !endGame)
			{
				endGame = true;
				gm.FinishGame();
				if (explosionFx != null)
				{
					GameObject fx = Instantiate(explosionFx, transform.position, Quaternion.identity);
					Destroy(fx, .5f);
				}
				Destroy(gameObject);
			}
            if (isEnemy)
            {
				if (explosionFx != null)
				{
					GameObject fx = Instantiate(explosionFx, fxPoint.position, Quaternion.identity);
					Destroy(fx, .5f);
				}
				gm.AddToScore(100);
				Destroy(gameObject);
            }
		}
		if (blinking)
		{
			SpriteBlinkingEffect();
		}
		if (Input.GetKeyDown(KeyCode.F1)) TakeDamage(1);
	}

	public void TakeDamage(int damage)
	{
		if (!canTakeDamage) return;

		HP -= damage;
		if (hasIframes)
		{
			canTakeDamage = false;
			StartCoroutine(RestartIframes());
		}
		if (blinkOnDamage) blinking = true;
		if(camShakeOnDamage)
        {
			gm.ShakeCamera(1f);
        }
        if (isPlayer)
        {
			if(HP <= Mathf.Floor((MaxHP/3) * 2) && HP > Mathf.Floor(MaxHP / 3))
            {
				pc.triggerState("Damaged");
			}
			else if (HP <= Mathf.Floor(MaxHP / 3))
			{
				pc.triggerState("Critical");
			}
		}
		else
		{
            if (enemy)
            {
				if (HP <= Mathf.Floor((MaxHP / 3) * 2) && HP > Mathf.Floor(MaxHP / 3))
				{
					enemy.triggerState("Damaged");
				}
				else if (HP <= Mathf.Floor(MaxHP / 3))
				{
					enemy.triggerState("Critical");
				}
			}
		}
	}

	public void selfDestruct()
    {
		if (explosionFx != null)
		{
			GameObject fx = Instantiate(explosionFx, fxPoint.position, Quaternion.identity);
			Destroy(fx, .5f);
		}
		Destroy(gameObject);
	}

	IEnumerator RestartIframes()
	{
		yield return new WaitForSeconds(iframeDuration);
		canTakeDamage = true;
	}

	private void SpriteBlinkingEffect()
	{
		spriteBlinkingTotalTimer += Time.deltaTime;
		if (spriteBlinkingTotalTimer >= iframeDuration)
		{
			blinking = false;
			spriteBlinkingTotalTimer = 0.0f;
			sprite.enabled = true;
			return;
		}

		spriteBlinkingTimer += Time.deltaTime;
		if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
		{
			spriteBlinkingTimer = 0.0f;
			if (sprite.enabled) sprite.enabled = false; 
			else sprite.enabled = true;
		}
	}

	void OnDestroy()
	{
		Destroy(transform.parent.gameObject);
	}
}
