using System.Collections;
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
    [SerializeField] GameObject magnetBody;

    private bool magnetActive = false;
    private float magnetPower = 1;
    private AudioSource audioSource;
    private GameObject[] bulletArray = new GameObject[7];
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = magnetBody.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
    }

    private void Update() {
        if (!magnetActive && magnetPower != 1) {
            magnetPower = Mathf.Clamp(magnetPower + (magnetRegen * Time.deltaTime), 0, 1);
            return;
        } 

        magnetPower = Mathf.Clamp(magnetPower - (magnetDrain * Time.deltaTime), 0, 1);

        if (magnetPower == 0) {
            SetMagnetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!magnetActive) {
            return;
        }

        if (other.gameObject.tag == "Enemy Projectile" && bulletArray[bulletArray.Length-1] == null) {
            if (other.GetComponent<Missile>()) {
                AddEnemyBulletToArray(Instantiate(missilePrefab, other.transform.position, Quaternion.identity));
            } else if (other.GetComponent<PiercingBullet>()) {
                AddEnemyBulletToArray(Instantiate(piercingBulletPrefab, other.transform.position, Quaternion.identity));
            } else {
                AddEnemyBulletToArray(Instantiate(basicBulletPrefab, other.transform.position, Quaternion.identity));
            }
            Destroy(other.gameObject);
        }
    }

    public float GetMagnetPower() { return magnetPower; }

    public void SetMagnetActive(bool magnetActive) {
        if (magnetActive) {
            audioSource.Play();
            spriteRenderer.enabled = magnetActive;
            StartCoroutine(ActivateMagnet());
        } else {
            StartCoroutine(DeactivateMagnet());
        }
    }

    public void LaunchBullet() {
        if (bulletArray[0] == null) {
            return;
        }

        GameObject bulletToFire = bulletArray[0];
        bulletToFire.transform.parent = null;
        Debug.Log(bulletToFire.transform.position);
        bulletToFire.GetComponent<Collider2D>().enabled = true;
        bulletToFire.GetComponent<Projectile>().enabled = true;
        bulletToFire.GetComponent<AudioSource>().enabled = true;
        bulletToFire.GetComponent<MagnetMovement>().enabled = false;
        bulletArray[0] = null;

        ResetBulletArray();
    }

    private void AddEnemyBulletToArray(GameObject gameObject) {
        for (int i = 0; i < bulletArray.Length; i++) {
            if (bulletArray[i] == null) {
                bulletArray[i] = gameObject;
                gameObject.transform.parent = bulletParent;
                gameObject.GetComponent<MagnetMovement>().SetMovementLocation(bulletLocations[i].localPosition);
                return;
            }
        }
    }

    private void ResetBulletArray() {
        for (int i = 1; i < bulletArray.Length; i++) {
            if (i != 0 && bulletArray[i] == null) {
                return;
            }
            int j = i-1;
            do {
                bulletArray[i-1] = bulletArray[i];
                bulletArray[i] = null;
                bulletArray[i-1].GetComponent<MagnetMovement>().SetMovementLocation(bulletLocations[i-1].localPosition);
                j--;
                if (j == -1) { break; }
            } while(bulletArray[j] == null);

        }
    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private IEnumerator ActivateMagnet() {
        this.magnetActive = true;
        while (magnetBody.transform.localScale != new Vector3(1,1,1)) {
            magnetBody.transform.localScale = new Vector3(Mathf.Clamp((float)(magnetBody.transform.localScale.x + (Time.deltaTime / 0.15)), 0.3f, 1),
                                               Mathf.Clamp((float)(magnetBody.transform.localScale.y + (Time.deltaTime / 0.15)), 0.3f, 1),
                                               1);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator DeactivateMagnet() {
        this.magnetActive = false;
        while (magnetBody.transform.localScale != new Vector3(0.3f,0.3f,1)) {
            magnetBody.transform.localScale = new Vector3(Mathf.Clamp((float)(magnetBody.transform.localScale.x - (Time.deltaTime / 0.15)), 0.3f, 1),
                                               Mathf.Clamp((float)(magnetBody.transform.localScale.y - (Time.deltaTime / 0.15)), 0.3f, 1),
                                               1);
            yield return new WaitForEndOfFrame();
        }
        spriteRenderer.enabled = false;
    }
}
