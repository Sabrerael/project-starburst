using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] WaveConfigSO bossWaveConfig;
    
    private WaveConfigSO currentWave;

    private void Start() {
        Time.timeScale = 5f;
        StartCoroutine(SpawnEnemyWaves());
    }

    private IEnumerator SpawnEnemyWaves() {
        // Just an initial wait
        yield return new WaitForSeconds(1);
        foreach (WaveConfigSO wave in waveConfigs) {
            currentWave = wave;

            for(int j = 0; j < currentWave.GetEnemyCount(); j++) {
                int pathNumber = currentWave.GetPathToSpawnEnemyOn(j);
                GameObject enemy = Instantiate(
                    currentWave.GetEnemyPrefab(j),
                    currentWave.GetStartingWaypoint(pathNumber).position,
                    currentWave.GetEnemyPrefab(j).transform.rotation,
                    transform);
                enemy.GetComponent<Pathfinder>().SetPathToFollow(pathNumber);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(currentWave.GetTimeAfterWave());
        }
        currentWave = bossWaveConfig;
        GameObject boss = Instantiate(currentWave.GetEnemyPrefab(0),
                    currentWave.GetStartingWaypoint(0).position,
                    Quaternion.identity,
                    transform);
        boss.GetComponent<Pathfinder>().SetPathToFollow(0);
    }

    public WaveConfigSO GetCurrentWave() { return currentWave; }
}
