using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] PowerUps;
    public GameObject Enemy;
    public Wave[] Waves;
    private int currentWave = 0;
    private Transform[] waveSpawnPoints;
    private Coroutine spawnCoroutine;

    void Start()
    {
        StartCoroutine(WaveManager());
    }

    private void Update()
    {
        
    }

    IEnumerator WaveManager()
    {
        while (currentWave < Waves.Length)
        {
            Debug.Log("Current wave: " + currentWave);
            SelectSpawnPoints(currentWave);

            for (int i = 0; i < Waves[currentWave].NumberOfPowerUp; i++)
            {
                RandomPowerUp();
            }

            spawnCoroutine = StartCoroutine(SpawnRoutine(currentWave));
            yield return spawnCoroutine;
            currentWave++;
        }
        Debug.Log("All waves completed!");
    }

    void SelectSpawnPoints(int waveIndex)
    {
        int count = Waves[waveIndex].NumberOfRandomSpawnPoint;
        waveSpawnPoints = new Transform[count];
        List<Transform> availableSpawnPoints = new List<Transform>(SpawnPoints);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            Debug.Log(randomIndex);
            waveSpawnPoints[i] = availableSpawnPoints[randomIndex];
            availableSpawnPoints.RemoveAt(randomIndex);
        }
    }

    void RandomSpawn(int waveIndex)
    {
        if (waveSpawnPoints == null || waveSpawnPoints.Length == 0)
        {
            return;
        }

        var spawnPoint = waveSpawnPoints[Random.Range(0, waveSpawnPoints.Length)];
        Instantiate(Enemy, spawnPoint.position, Quaternion.identity);
    }

    void RandomPowerUp()
    {
        if (PowerUps.Length == 0 || SpawnPoints.Length == 0)
        {
            return;
        }

        var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        var powerUpPrefab = PowerUps[Random.Range(0, PowerUps.Length)];
        Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine(int waveIndex)
    {
        yield return new WaitForSeconds(Waves[waveIndex].DelayStart);

        for (int i = 0; i < Waves[waveIndex].TotalSpawnEnemies; i++)
        {
            RandomSpawn(waveIndex);
            yield return new WaitForSeconds(Waves[waveIndex].SpawnInterval);
        }
    }
}
