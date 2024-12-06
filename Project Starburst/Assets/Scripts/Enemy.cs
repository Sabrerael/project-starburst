using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Vector3 projectileSpawnPointModification;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int scoreValue = 100;
    [SerializeField] float fireInterval = 2;
    [SerializeField] Material defaultMaterial;
    [SerializeField] bool firing = true;

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

    public void ToggleFiring() {
        firing = !firing;
    }

    protected virtual IEnumerator FireProjectiles(float time) {
        yield return new WaitForSeconds(0.5f);
        while (true) {
            if (firing) {
                Instantiate(projectile, transform.position + projectileSpawnPointModification, transform.rotation);
                yield return new WaitForSeconds(time);
            } else {
                yield return new WaitForSeconds(time);
            }
        }
    }
}
