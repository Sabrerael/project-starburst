using UnityEngine;

public class BulletMagnet : MonoBehaviour {
    [SerializeField] Transform bulletParent;

    [Header("Bullet Prefabs")]
    [SerializeField] GameObject basicBulletPrefab;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject piercingBulletPrefab;

    [Header("Magnet Properties")]
    [SerializeField] float magnetDrain = 2;
    [SerializeField] float magnetRegen = 4;
    [SerializeField] Transform[] bulletLocations;

    private bool magnetActive = false;
    private float magnetPower = 1;
    private AudioSource audioSource;
    private GameObject[] bulletArray = new GameObject[7];
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private void Update() {
        if (!magnetActive && magnetPower != 1) {
            magnetPower = Mathf.Clamp(magnetPower + (magnetRegen * Time.deltaTime), 0, 1);
            return;
        } 

        magnetPower = Mathf.Clamp(magnetPower - (magnetDrain * Time.deltaTime), 0, 1);

        if (magnetPower == 0) {
            magnetActive = false;
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!magnetActive) {
            return;
        }

        if (other.gameObject.tag == "Enemy Projectile" && bulletArray[4] == null) {
            if (other.GetComponent<Missile>()) {
                AddEnemyBulletToArray(Instantiate(missilePrefab));
            } else if (other.GetComponent<PiercingBullet>()) {
                AddEnemyBulletToArray(Instantiate(piercingBulletPrefab));
            } else {
                AddEnemyBulletToArray(Instantiate(basicBulletPrefab));
            }
            Destroy(other.gameObject);
        }
    }

    public float GetMagnetPower() { return magnetPower; }

    public void SetMagnetActive(bool magnetActive) {
        this.magnetActive = magnetActive;
        spriteRenderer.enabled = magnetActive;
        if (magnetActive) {
            audioSource.Play();
        }
    }

    public void LaunchBullet() {
        if (bulletArray[0] == null) {
            Debug.Log("No Bullet to fire");
            return;
        }

        GameObject bulletToFire = bulletArray[0];
        bulletToFire.transform.parent = null;
        bulletToFire.GetComponent<Collider2D>().enabled = true;
        bulletToFire.GetComponent<Projectile>().enabled = true;

        ResetBulletArray();
    }

    private void AddEnemyBulletToArray(GameObject gameObject) {
        for (int i = 0; i < bulletArray.Length; i++) {
            if (bulletArray[i] == null) {
                bulletArray[i] = gameObject;
                gameObject.transform.parent = bulletParent;
                gameObject.transform.position = bulletLocations[i].position;
                return;
            }
        }
    }

    private void ResetBulletArray() {
        for (int i = 1; i < bulletArray.Length; i++) {
            if (bulletArray[i] == null) {
                return;
            }
            bulletArray[i-1] = bulletArray[i];
            bulletArray[i-1].transform.position = bulletLocations[i-1].position;
        }
    }
}
