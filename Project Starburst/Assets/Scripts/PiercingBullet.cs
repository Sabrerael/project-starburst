using UnityEngine;

public class PiercingBullet : Projectile {

    protected override void OnCollisionEnter2D(Collision2D other) {
        // Overriding this to make it blank
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if ((isPlayerProjectile && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")) ||  
             other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage, bulletType);
            GameObject particles = Instantiate(particlePrefab, transform.position,Quaternion.identity);
            Destroy(particles, 0.5f);
        }
    }
}
