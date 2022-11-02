using UnityEngine;

public class Missile : Projectile {
    [SerializeField] GameObject missileExplosion;

    protected override void OnCollisionEnter2D(Collision2D other) {
        if ((isPlayerProjectile && other.gameObject.tag == "Enemy") || other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage);
            Debug.Log("Spawn explosion here");
            Destroy(gameObject);
        }
    }
}
