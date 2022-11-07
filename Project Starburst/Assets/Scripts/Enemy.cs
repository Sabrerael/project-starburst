using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] GameObject projectile;
    [SerializeField] int scoreValue = 100;
    [SerializeField] float fireInterval = 2;

    private Health health;

    private void Start() {
        health = GetComponent<Health>();
        StartCoroutine(FireProjectiles(fireInterval));
    }

    public void AddScoreToPlayer() {
        FindObjectOfType<Player>().AddToTotalScore(scoreValue);
    }

    private IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
