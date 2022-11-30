using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject {
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 2f;
    [SerializeField] float timeAfterWave = 1f;
    [SerializeField] bool continuePathfinding = false;
    [SerializeField] int waypointIndexToReturnTo = 1;

    public int GetEnemyCount() { return enemyPrefabs.Count; }

    public GameObject GetEnemyPrefab(int index) { return enemyPrefabs[index]; }

    public Transform GetStartingWaypoint() { return pathPrefab.GetChild(0); }

    public float GetMoveSpeed() { return moveSpeed; }

    public float GetTimeAfterWave() { return timeAfterWave; }

    public bool IsContinuePathfinding() { return continuePathfinding; }

    public int GetWaypointIndexToReturnTo() { return waypointIndexToReturnTo;}

    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab) {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetRandomSpawnTime() {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
                                       timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
