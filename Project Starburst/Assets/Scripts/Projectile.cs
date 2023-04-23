using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] protected bool isPlayerProjectile = false;
    [SerializeField] GameObject magnetEffects;
    [SerializeField] float movementSpeed = -2.5f;
    [SerializeField] protected int damage = 50;
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material colorblindMaterial;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected TrailRenderer trailRenderer;
    [SerializeField] protected BulletType bulletType = BulletType.Basic;
    
    private AudioSource audioSource;
    private float xMovementSpeed;
    private float yMovementSpeed;

    private void Awake() {
        SetMaterial();
    }

    protected virtual void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        //SettingsManager.onSettingsChange += SetVolume;
        SettingsManager.onSettingsChange += SetMaterial;

        xMovementSpeed = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * movementSpeed * -1;
        yMovementSpeed = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * movementSpeed;
    }

    private void Update() {
        transform.position += new Vector3(xMovementSpeed * Time.deltaTime, yMovementSpeed * Time.deltaTime, 0);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if ((isPlayerProjectile && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")) ||  
             other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damage, bulletType);
            Destroy(gameObject);
        }
    }

    public bool IsPlayerProjectile() { return isPlayerProjectile; }

    public int GetDamage() { return damage; }

    public GameObject GetMagnetEffects() { return magnetEffects; }

    public void SetPlayerProjectile(bool isPlayerProjectile) { this.isPlayerProjectile = isPlayerProjectile; }

    private void SetMaterial() {
        //if (SettingsManager.GetColorSet() != 0) {
            //spriteRenderer.material = colorblindMaterial;
            //trailRenderer.material = colorblindMaterial;
        //} else {
        //    spriteRenderer.material = defaultMaterial;
        //    trailRenderer.material = defaultMaterial;
        //}
    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
