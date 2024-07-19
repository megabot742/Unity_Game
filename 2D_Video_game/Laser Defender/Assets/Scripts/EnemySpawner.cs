using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] WaveConfigSO currentWave;
    // Start is called before the first frame update
    [SerializeField] bool isLooping = false;
    void Start()
    {
        StartCoroutine(SpawnEnemiesWaves());
    }
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    IEnumerator SpawnEnemiesWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for(int i = 0; i< currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),currentWave.GetStartingWaypoint().position,Quaternion.identity);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        while(isLooping);
           
    }
}
