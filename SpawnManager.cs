using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Prefabs
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] powerupPrefabs;

    // Attributes
    private float nextEnemySpawn;
    private float enemySpawnRate;

    private float nextPowerUpSpawn;
    private float powerUpSpawnRate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextEnemySpawn)
        {
            SpawnEnemy();
        }

        if (Time.time > nextPowerUpSpawn)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        enemySpawnRate = Random.Range(1f, 5f);
        nextEnemySpawn = Time.time + enemySpawnRate;

        Vector3 randomEnemyPos = new Vector3(Random.Range(-2f, 2f), 6f, 0);

        Instantiate(enemyPrefabs[randomEnemy], randomEnemyPos, Quaternion.identity);
    }

    private void SpawnPowerUp()
    {
        int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
        powerUpSpawnRate = Random.Range(4f, 9f);
        nextPowerUpSpawn = Time.time + powerUpSpawnRate;

        Vector3 randomPowerUpPos = new Vector3(Random.Range(-2f, 2f), 6f, 0);

        Instantiate(powerupPrefabs[randomPowerUp], randomPowerUpPos, Quaternion.identity);
    }





    
}
