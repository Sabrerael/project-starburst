using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    private EnemySpawner enemySpawner;
    private WaveConfigSO waveConfig;
    private List<Transform> waypoints;
    private int waypointIndex = 0;
    private bool continuePathfinding;
    private int waypointIndexToReturnTo;

    private void Awake() {
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    }

    private void Start() {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
        continuePathfinding = waveConfig.IsContinuePathfinding();
        waypointIndexToReturnTo = waveConfig.GetWaypointIndexToReturnTo();
    }

    private void Update() {
        FollowPath();
    }

    private void FollowPath() {
        if (waypointIndex < waypoints.Count) {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        } else if (continuePathfinding && waypointIndex >= waypoints.Count) {
            waypointIndex = waypointIndexToReturnTo;
        } else {
            Destroy(gameObject);
        }
    }
}