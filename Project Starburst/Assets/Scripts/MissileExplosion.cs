using UnityEngine;

public class MissileExplosion : MonoBehaviour {
    [SerializeField] float lifetime = 0.25f;
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material colorblindMaterial;
    [SerializeField] protected SpriteRenderer spriteRenderer;
 
    private int damage = 100;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SetMaterial();
        SettingsManager.onSettingsChange += SetVolume;
        SettingsManager.onSettingsChange += SetMaterial;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage);
        }
    }

    public void SetDamage(int damage) { this.damage = damage; }

    private void SetMaterial() {
        if (SettingsManager.GetColorSet() != 0) {
            spriteRenderer.material = colorblindMaterial;
        } else {
            spriteRenderer.material = defaultMaterial;
        }
    }
    
    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
