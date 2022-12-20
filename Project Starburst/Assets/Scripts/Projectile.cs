using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] protected bool isPlayerProjectile = false;
    [SerializeField] float movementSpeed = -2.5f;
    [SerializeField] protected int damage = 50;
    
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private void Update() {
        transform.position += new Vector3(0, movementSpeed * Time.deltaTime, 0);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if ((isPlayerProjectile && other.gameObject.tag == "Enemy") || other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage);
            Destroy(gameObject);
        }
    }

    public bool IsPlayerProjectile() { return isPlayerProjectile; }
    public int GetDamage() { return damage; }

    public void SetPlayerProjectile(bool isPlayerProjectile) { this.isPlayerProjectile = isPlayerProjectile; }
}
