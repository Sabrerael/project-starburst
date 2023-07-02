using System.Collections;
using UnityEngine;

public class BurstFireEnemy : Enemy {
    [SerializeField] float timeBetweenShotsInBurst = 0.5f;

    protected override IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + projectileSpawnPointModification, transform.rotation);
            yield return new WaitForSeconds(timeBetweenShotsInBurst);
            Instantiate(projectile, transform.position + projectileSpawnPointModification, transform.rotation);
            yield return new WaitForSeconds(time);
        }
    }
}
