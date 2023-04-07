using System.Collections;
using UnityEngine;

public class TriShotEnemy : Enemy {
    [SerializeField] float angle = 25f;

    protected override IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + projectileSpawnPointModification + new Vector3(0.2f,0,0), Quaternion.Euler(0,0,-angle));
            Instantiate(projectile, transform.position + projectileSpawnPointModification, Quaternion.identity);
            Instantiate(projectile, transform.position + projectileSpawnPointModification + new Vector3(-0.2f,0,0), Quaternion.Euler(0,0,angle));
            yield return new WaitForSeconds(time);
        }
    }
}
