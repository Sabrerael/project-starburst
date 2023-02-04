using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] WaveConfigSO bossWaveConfig;
    
    private WaveConfigSO currentWave;

    private void Start() {
        //Time.timeScale = 5f;
        StartCoroutine(SpawnEnemyWaves());
    }

    private IEnumerator SpawnEnemyWaves() {
        // Just an initial wait
        yield return new WaitForSeconds(1);
        foreach (WaveConfigSO wave in waveConfigs) {
            currentWave = wave;

            for(int j = 0; j < currentWave.GetEnemyCount(); j++) {
                Instantiate(
                    currentWave.GetEnemyPrefab(j),
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity,
                    transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(currentWave.GetTimeAfterWave());
        }
        currentWave = bossWaveConfig;
        Instantiate(currentWave.GetEnemyPrefab(0),
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity,
                    transform);
    }

    public WaveConfigSO GetCurrentWave() { return currentWave; }
}
