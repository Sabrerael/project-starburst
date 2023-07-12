using UnityEngine;

public class ProxyMine : Projectile {
    [SerializeField] float detonationTimer = 0.4f;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip triggeringSound;

    private bool triggered = false;
    private float timer = 0;

    protected override void Update() {
        base.Update();
        if (triggered) {
            timer += Time.deltaTime;
        }
        if (timer >= detonationTimer) {
            TriggerExplosion();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        // Overriding this to make it blank
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if ((isPlayerProjectile && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")) || ( 
             !isPlayerProjectile && other.gameObject.tag == "Player")) {
            audioSource.clip = triggeringSound;
            audioSource.Play();
            triggered = true;
        }
    }

    private void TriggerExplosion() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
