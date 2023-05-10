using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    private  int enemyWave= 1;
    public int enemyCount;
    
    void Start()
    {
        SpawnEnemyWave(enemyWave);
        SpawnPowerup();
    }

    
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if(enemyCount == 0) {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            SpawnPowerup();
           
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {   
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenererateSpawnPosition(), enemyPrefab.transform.rotation);
           
        }
       
    }
    void SpawnPowerup()
    {
        Instantiate(powerupPrefab,GenererateSpawnPosition(),powerupPrefab.transform.rotation);
    }
    private Vector3 GenererateSpawnPosition()
    {

        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
