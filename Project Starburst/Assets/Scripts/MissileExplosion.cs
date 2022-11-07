using UnityEngine;

public class MissileExplosion : MonoBehaviour {
    [SerializeField] float lifetime = 0.25f;
    [SerializeField] AudioClip explosionSound;

    private int damage = 100;

    private void Start() {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage);
        }
    }

    public void SetDamage(int damage) { this.damage = damage; }
}
