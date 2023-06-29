using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Vector3 projectileSpawnPointModification;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int scoreValue = 100;
    [SerializeField] float fireInterval = 2;
    [SerializeField] Material defaultMaterial;

    private Health health;

    private void Start() {
        health = GetComponent<Health>();
        StartCoroutine(FireProjectiles(fireInterval));
        if (projectileSpawnPointModification == null) {
            projectileSpawnPointModification = new Vector3(0,-0.5f, 0);
        }
    }

    public void AddScoreToPlayer() {
        FindObjectOfType<Player>().AddToTotalScore(scoreValue);
    }

    protected virtual IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + projectileSpawnPointModification, transform.rotation);
            yield return new WaitForSeconds(time);
        }
    }
}
