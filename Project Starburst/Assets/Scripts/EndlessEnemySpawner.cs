using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndlessEnemySpawner : EnemySpawner {
    [SerializeField] Transform[] paths;
    [SerializeField] TextMeshProUGUI scoreValue;

    private Transform currentPath;
    private int waveCounter;
    

    private void Update() {
        scoreValue.text = waveCounter == 0 ? "01" : waveCounter.ToString("00");
    }

    override protected IEnumerator SpawnEnemyWaves() {
        // Just an initial wait
        yield return new WaitForSeconds(1);
        while (true) {
            waveCounter++;
            player.IncrementWavesSurvived();
            currentWave = waveConfigs[Random.Range(0, waveConfigs.Count-1)];
            currentPath = paths[Random.Range(0, paths.Length-1)];

            for(int j = 0; j < currentWave.GetEnemyCount(); j++) {
                int pathNumber = currentWave.GetPathToSpawnEnemyOn(j);
                GameObject enemy = Instantiate(
                    currentWave.GetEnemyPrefab(j),
                    currentPath.GetChild(0).position,
                    currentPath.GetChild(0).rotation,
                    transform);
                enemy.GetComponent<Pathfinder>().SetPathToFollow(0);
                enemy.GetComponent<Pathfinder>().SetWaypoints(currentPath);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(currentWave.GetTimeAfterWave());
        }
    }

    public int GetWaveCounter() {return waveCounter; }
}
