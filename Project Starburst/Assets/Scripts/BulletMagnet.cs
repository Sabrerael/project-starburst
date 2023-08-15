using System.Collections;
using UnityEngine;

public class BulletMagnet : MonoBehaviour {
    [SerializeField] Transform bulletParent;

    [Header("Bullet Prefabs")]
    [SerializeField] GameObject basicBulletPrefab;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject piercingBulletPrefab;
    [SerializeField] GameObject spreadshotPrefab;
    [SerializeField] GameObject trackingPrefab;
    [SerializeField] GameObject minePrefab;
    [SerializeField] GameObject boomerangPrefab;

    [Header("Magnet Properties")]
    [SerializeField] float magnetDrain = 2;
    [SerializeField] float magnetRegen = 4;
    [SerializeField] Transform[] bulletLocations;
    [SerializeField] SpriteRenderer[] ammoIcons;
    [SerializeField] Sprite[] ammoIconSprites;
    [SerializeField] GameObject magnetBody;
    [SerializeField] AudioClip magnetActivateSound;
    [SerializeField] AudioClip magnetGrabSound;
    [SerializeField] GameObject targetMarker;

    private bool magnetActive = false;
    private float magnetPower = 1f;
    private AudioSource audioSource;
    private GameObject[] bulletArray = new GameObject[7];
    private SpriteRenderer spriteRenderer;
    private GameObject target;
    private bool targetChanged = true;
    private GameObject markerInstance;
    private GameObject enemySpawner;

    private void Start() {
        spriteRenderer = magnetBody.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
        enemySpawner = GameObject.Find("Enemy Spawner");
    }

    private void Update() {
        if (bulletArray[0] && bulletArray[0].GetComponent<TrackingBullet>()) {
            SetTarget();
        }

        if (!magnetActive && magnetPower != 1) {
            magnetPower = Mathf.Clamp(magnetPower + (magnetRegen * Time.deltaTime), 0, 1);
            return;
        }

        if (magnetActive) {
            magnetPower = Mathf.Clamp(magnetPower - (magnetDrain * Time.deltaTime), 0, 1);
        }

        if (magnetPower == 0) {
            SetMagnetActive(false);
        }
    }

    private void OnDestroy() {
        SettingsManager.onSettingsChange -= SetVolume;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!magnetActive) {
            return;
        }

        if (other.gameObject.tag == "Enemy Projectile" && bulletArray[bulletArray.Length-1] == null) {
            if (other.GetComponent<Missile>()) {
                AddEnemyBulletToArray(Instantiate(missilePrefab, other.transform.position, Quaternion.identity));
            } else if (other.GetComponent<PiercingBullet>()) {
                AddEnemyBulletToArray(Instantiate(piercingBulletPrefab, other.transform.position, Quaternion.identity));
            } else if (other.GetComponent<Spreadshot>()) {
                AddEnemyBulletToArray(Instantiate(spreadshotPrefab, other.transform.position, Quaternion.identity));
            } else if (other.GetComponent<TrackingBullet>()) {
                AddEnemyBulletToArray(Instantiate(trackingPrefab, other.transform.position, Quaternion.identity));
            } else if (other.GetComponent<ProxyMine>()) {
                AddEnemyBulletToArray(Instantiate(minePrefab, other.transform.position, Quaternion.identity));
            } else if (other.GetComponent<BoomerangBullet>()) {
                AddEnemyBulletToArray(Instantiate(boomerangPrefab, other.transform.position, Quaternion.identity));
            } else {
                AddEnemyBulletToArray(Instantiate(basicBulletPrefab, other.transform.position, Quaternion.identity));
            }
            Destroy(other.gameObject);
        }
    }

    public float GetMagnetPower() { return magnetPower; }

    public void SetMagnetActive(bool magnetActive) {
        if (magnetActive) {
            audioSource.clip = magnetActivateSound;
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
        var trackingBullet = bulletToFire.GetComponent<TrackingBullet>();
        if (trackingBullet && !trackingBullet.HasTarget()) {
            return;
        }

        bulletToFire.transform.parent = null;
        bulletToFire.GetComponent<Collider2D>().enabled = true;
        Projectile projectile = bulletToFire.GetComponent<Projectile>();
        projectile.enabled = true;
        projectile.GetMagnetEffects().SetActive(false);
        bulletToFire.GetComponent<AudioSource>().enabled = true;
        bulletToFire.GetComponent<MagnetMovement>().enabled = false;
        if (bulletToFire.GetComponent<BoomerangBullet>()) {
            bulletToFire.GetComponent<Animator>().SetTrigger("Boomerang");
        } else {
            bulletToFire.GetComponent<Animator>().enabled = false;
        }
        if (trackingBullet) { trackingBullet.HasFired(); }
        bulletArray[0] = null;

        targetChanged = true;
        ResetBulletArray();
    }

    public void ClearBulletArray() {
        for (int i = 0; i < bulletArray.Length; i++) {
            if (bulletArray[i] != null) {
                GameObject.Destroy(bulletArray[i]);
            }
        }
    }

    private void AddEnemyBulletToArray(GameObject gameObject) {
        for (int i = 0; i < bulletArray.Length; i++) {
            if (bulletArray[i] == null) {
                audioSource.clip = magnetGrabSound;
                audioSource.Play();
                bulletArray[i] = gameObject;
                gameObject.transform.parent = bulletParent;
                gameObject.GetComponent<MagnetMovement>().SetMovementLocation(bulletLocations[i].localPosition);
                ammoIcons[i].sprite = ammoIconSprites[1];
                return;
            }
        }
    }

    private void ResetBulletArray() {
        for (int i = 1; i < bulletArray.Length; i++) {
            if (bulletArray[i] == null) {
                ammoIcons[i-1].sprite = ammoIconSprites[0];
                return;
            } else if (i == 6) {
                ammoIcons[i].sprite = ammoIconSprites[0];
            }
            
            int j = i-1;
            do {
                bulletArray[i-1] = bulletArray[i];
                bulletArray[i] = null;
                bulletArray[i-1].GetComponent<MagnetMovement>().SetMovementLocation(bulletLocations[i-1].localPosition);
                j--;
                if (j == -1) { break; }
            } while(bulletArray[j] == null);
            if (i == 1 && bulletArray[i-1].GetComponent<TrackingBullet>()) {
                SetTarget();
            } else if (i == 1 && !bulletArray[i-1].GetComponent<TrackingBullet>()) {
                Destroy(markerInstance);
            }
        }
    }

    private void SetTarget() {
        foreach (var enemy in enemySpawner.transform.GetComponentsInChildren<Transform>()) {
            if (enemy.name == "Enemy Spawner") {
                continue;
            } else if (target == null) {
                target = enemy.gameObject;
                targetChanged = true;
            } else {
                var currentTargetDistance = (target.transform.position - transform.position).magnitude;
                var newTargetDistance = (enemy.transform.position - transform.position).magnitude;
                if (currentTargetDistance > newTargetDistance) {
                    target = enemy.gameObject;
                    targetChanged = true;
                }
            }
        }
            
        if (targetChanged && target) {
            var trackingBullet = bulletArray[0].GetComponent<TrackingBullet>();
            trackingBullet.SetTarget(target);
            if (markerInstance) {
                markerInstance.transform.position = target.transform.position;
                markerInstance.transform.parent = target.transform;
            } else {
                markerInstance = Instantiate(targetMarker, target.transform.position, Quaternion.identity, target.transform);
            }
            targetChanged = false;
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
