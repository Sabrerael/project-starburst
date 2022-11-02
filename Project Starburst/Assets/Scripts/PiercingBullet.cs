using UnityEngine;

public class PiercingBullet : Projectile {

    protected override void OnCollisionEnter2D(Collision2D other) {
        if ((isPlayerProjectile && other.gameObject.tag == "Enemy") || other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage);
        } 
    }
}
