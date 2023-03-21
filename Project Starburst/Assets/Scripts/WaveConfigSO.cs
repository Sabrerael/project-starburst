using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject {
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform path1Prefab;
    [SerializeField] Transform path2Prefab;
    [SerializeField] List<int> pathToSpawnEnemyOn;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 2f;
    [SerializeField] float timeAfterWave = 1f;
    [SerializeField] bool continuePathfinding = false;
    [SerializeField] int waypointIndexToReturnTo = 1;

    public int GetEnemyCount() { return enemyPrefabs.Count; }

    public GameObject GetEnemyPrefab(int index) { return enemyPrefabs[index]; }

    public Transform GetStartingWaypoint(int pathNumber) { 
        if (pathNumber == 0) {
            return path1Prefab.GetChild(0);
        } else {
            return path2Prefab.GetChild(0);
        }
    }

    public int GetPathToSpawnEnemyOn(int index) { return pathToSpawnEnemyOn[index]; }

    public float GetMoveSpeed() { return moveSpeed; }

    public float GetTimeAfterWave() { return timeAfterWave; }

    public bool IsContinuePathfinding() { return continuePathfinding; }

    public int GetWaypointIndexToReturnTo() { return waypointIndexToReturnTo;}

    public List<Transform> GetPath1Waypoints() {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in path1Prefab) {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public List<Transform> GetPath2Waypoints() {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in path2Prefab) {
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
