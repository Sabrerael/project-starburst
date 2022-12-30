using UnityEngine;

public class MissileExplosion : MonoBehaviour {
    [SerializeField] float lifetime = 0.25f;

    private int damage = 100;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage);
        }
    }

    public void SetDamage(int damage) { this.damage = damage; }
    
    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
