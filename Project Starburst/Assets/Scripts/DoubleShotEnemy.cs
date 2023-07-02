using System.Collections;
using UnityEngine;

public class DoubleShotEnemy : Enemy {
    protected override IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + projectileSpawnPointModification + new Vector3(0.25f,0,0), transform.rotation);
            Instantiate(projectile, transform.position + projectileSpawnPointModification - new Vector3(0.25f,0,0), transform.rotation);
            yield return new WaitForSeconds(time);
        }
    }
}
