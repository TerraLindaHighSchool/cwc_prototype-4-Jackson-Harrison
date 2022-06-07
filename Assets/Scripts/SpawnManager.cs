using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;
    public GameObject enemyPrefab;
    public GameObject heavyPrefab;
    public TextMeshProUGUI WaveNumberUI;
    private float spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPostion(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        Debug.Log(enemyCount);
        UpdateWaveNumberUI();

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPostion(), powerupPrefab.transform.rotation);
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
    }

    // Update the Wave Number UI
    private void UpdateWaveNumberUI()
    {
        WaveNumberUI.text = "Wave " + waveNumber;
    }

    void SpawnEnemyWave(int waveNumber)
    {

        for (int i = 0; i < (waveNumber - 3); i++)
        {
            Instantiate(heavyPrefab, GenerateSpawnPostion(), heavyPrefab.transform.rotation);
        }

        for (int i = 0; i < waveNumber; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPostion(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPostion()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}