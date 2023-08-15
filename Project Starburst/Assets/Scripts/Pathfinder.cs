using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    private EnemySpawner enemySpawner;
    private WaveConfigSO waveConfig;
    private List<Transform> waypoints;
    private int pathToFollow;
    private int waypointIndex = 0;
    private bool continuePathfinding;
    private int waypointIndexToReturnTo;

    private void Awake() {
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    }

    private void Start() {
        waveConfig = enemySpawner.GetCurrentWave();
        if (pathToFollow == 1) {
            waypoints = waveConfig.GetPath2Waypoints();
        } else {
            waypoints = waveConfig.GetPath1Waypoints();
        }
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
            Quaternion targetRotation = waypoints[waypointIndex].rotation;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, .75f);
            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        } else if (continuePathfinding && waypointIndex >= waypoints.Count) {
            waypointIndex = waypointIndexToReturnTo;
        } else {
            Destroy(gameObject);
        }
    }

    public void SetPathToFollow(int pathIndex) { pathToFollow = pathIndex; }
    public void SetWaypoints(Transform waypointParent) { 
        waypoints = new List<Transform>();
        foreach (Transform child in waypointParent) {
            waypoints.Add(child);
        }
    }
}
