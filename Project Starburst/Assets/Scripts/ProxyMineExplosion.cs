using UnityEngine;

public class ProxyMineExplosion : MonoBehaviour {
    [SerializeField] float lifetime = 0.25f;
    [SerializeField] protected Material defaultMaterial;
 
    private BulletType bulletType = BulletType.ProxyMine;
    private int damage = 150;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player" || other.gameObject.tag == "Boss") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage, bulletType);
        }
    }

    public void SetDamage(int damage) { this.damage = damage; }
    
    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
