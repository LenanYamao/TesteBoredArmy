using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float delayBetweenSpawn = 1f;

    public GameObject enemyChaser;
    public GameObject enemyShooter;

    public List<Transform> spawnPoints;

    public Transform player;
    public GameManager gm;

    public void Start()
    {
        delayBetweenSpawn = PlayerPrefs.GetFloat("spawnDelay");

        InvokeRepeating("SpawnWithDelay", 1f, delayBetweenSpawn);
    }
    void SpawnWithDelay()
    {
        int rand = Random.Range(0, spawnPoints.Count);
        int chooseEnemy = Random.Range(0, 2);

        GameObject enemy;
        if (chooseEnemy == 0) enemy = enemyChaser;
        else enemy = enemyShooter;

        var spawned = Instantiate(enemy, spawnPoints[rand].position, Quaternion.identity);
        var enemyScript = spawned.GetComponentInChildren<Enemy>();
        var damageable = spawned.GetComponentInChildren<DamageableEntity>();
        enemyScript.playerTransform = player;
        damageable.gm = gm;
    }
}
