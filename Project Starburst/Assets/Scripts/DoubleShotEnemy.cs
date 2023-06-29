using System.Collections;
using UnityEngine;

public class DoubleShotEnemy : Enemy {
    protected override IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + projectileSpawnPointModification + new Vector3(0.25f,0,0), Quaternion.identity);
            Instantiate(projectile, transform.position + projectileSpawnPointModification - new Vector3(0.25f,0,0), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
