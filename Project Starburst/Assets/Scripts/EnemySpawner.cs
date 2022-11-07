using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfigSO> waveConfigs;
    
    private WaveConfigSO currentWave;

    private void Start() {
        StartCoroutine(SpawnEnemyWaves());
    }

    private IEnumerator SpawnEnemyWaves() {
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
    }

    public WaveConfigSO GetCurrentWave() { return currentWave; }
}
