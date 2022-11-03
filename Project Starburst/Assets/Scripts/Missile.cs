using UnityEngine;

public class Missile : Projectile {
    [SerializeField] MissileExplosion missileExplosionPrefab;

    protected override void OnCollisionEnter2D(Collision2D other) {
        if ((isPlayerProjectile && other.gameObject.tag == "Enemy") || other.gameObject.tag == "Player") {
            MissileExplosion missionExplosion = Instantiate(missileExplosionPrefab, transform.position, Quaternion.identity);
            missionExplosion.SetDamage(damage);
            Destroy(gameObject);
        }
    }
}
