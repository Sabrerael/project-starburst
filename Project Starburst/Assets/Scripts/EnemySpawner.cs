using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] protected List<WaveConfigSO> waveConfigs;
    [SerializeField] WaveConfigSO bossWaveConfig;
    
    protected WaveConfigSO currentWave;
    protected Player player;

    private void Start() {
        //Time.timeScale = 5f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.ResetPlayer();
        StartCoroutine(SpawnEnemyWaves());
    }

    protected virtual IEnumerator SpawnEnemyWaves() {
        // Just an initial wait
        yield return new WaitForSeconds(1);
        foreach (WaveConfigSO wave in waveConfigs) {
            currentWave = wave;

            for(int j = 0; j < currentWave.GetEnemyCount(); j++) {
                int pathNumber = currentWave.GetPathToSpawnEnemyOn(j);
                GameObject enemy = Instantiate(
                    currentWave.GetEnemyPrefab(j),
                    currentWave.GetStartingWaypoint(pathNumber).position,
                    currentWave.GetStartingWaypoint(pathNumber).rotation,
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

    private IEnumerator SpawnSingleEnemyWave(WaveConfigSO waveConfig) {
        currentWave = waveConfig;

        for(int j = 0; j < currentWave.GetEnemyCount(); j++) {
            int pathNumber = currentWave.GetPathToSpawnEnemyOn(j);
            GameObject enemy = Instantiate(
                currentWave.GetEnemyPrefab(j),
                currentWave.GetStartingWaypoint(pathNumber).position,
                currentWave.GetStartingWaypoint(pathNumber).rotation,
                transform);
            enemy.GetComponent<Pathfinder>().SetPathToFollow(pathNumber);
            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
    }

    public WaveConfigSO GetCurrentWave() { return currentWave; }

    public void StartWaveConfig(WaveConfigSO waveConfig) {
        StartCoroutine(SpawnSingleEnemyWave(waveConfig));
    }
}
