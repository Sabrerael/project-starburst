using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] protected bool isPlayerProjectile = false;
    [SerializeField] GameObject magnetEffects;
    [SerializeField] protected float movementSpeed = -2.5f;
    [SerializeField] protected int damage = 50;
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected GameObject particlePrefab;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected TrailRenderer trailRenderer;
    [SerializeField] protected BulletType bulletType = BulletType.Basic;
    [SerializeField] protected AudioClip[] audioClips;
    
    protected AudioSource audioSource;
    protected float xMovementSpeed;
    protected float yMovementSpeed;

    protected virtual void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();

        xMovementSpeed = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * movementSpeed * -1;
        yMovementSpeed = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * movementSpeed;
        PlayRandomClip();
    }

    protected virtual void Update() {
        transform.position += new Vector3(xMovementSpeed * Time.deltaTime, yMovementSpeed * Time.deltaTime, 0);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if ((isPlayerProjectile && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")) ||  
             other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage, bulletType);
            if (particlePrefab) {
                GameObject particles = Instantiate(particlePrefab, transform.position,Quaternion.identity);
                Destroy(particles, 0.5f);
            }
            Destroy(gameObject);
        }
    }

    public bool IsPlayerProjectile() { return isPlayerProjectile; }

    public int GetDamage() { return damage; }

    public GameObject GetMagnetEffects() { return magnetEffects; }

    public void SetPlayerProjectile(bool isPlayerProjectile) { this.isPlayerProjectile = isPlayerProjectile; }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private void PlayRandomClip() {
        audioSource.clip = audioClips[Random.Range(0,3)];
        audioSource.Play();
    }
}
