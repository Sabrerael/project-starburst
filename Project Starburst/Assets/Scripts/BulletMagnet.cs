using UnityEngine;

public class BulletMagnet : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy Projectile") {
            Debug.Log("Bullet should be grabbed by player");
        }
    }
}
