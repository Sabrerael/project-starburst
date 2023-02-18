using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;
    [SerializeField] bool applyCameraShake = false;
    [SerializeField] GameObject deathSoundEffectObject;
    [SerializeField] GameObject hitParticleEffect;
    [SerializeField] GameObject hitParticleEffectColorBlind;

    [Header("Enemy Specific Properties")]
    [SerializeField] BulletType shieldWeakness = BulletType.None;
    [SerializeField] GameObject shieldObject;
    [SerializeField] GameObject brokenShieldObject;

    [Header("Player Specific Properties")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem playerParticleSystem;

    private Animator animator;
    private AudioSource audioSource;
    private CameraShake cameraShake;
    private Movement movement;
    private int totalHealthPoints;
    private int currentHealthPoints;

    private void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Start() {
        totalHealthPoints = healthPoints;
        currentHealthPoints = healthPoints;
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
    }

    public int GetTotalHealthPoints() { return totalHealthPoints; }
    public int GetCurrentHealthPoints() { return currentHealthPoints; }
    public float GetFraction() { return (float)currentHealthPoints / (float)totalHealthPoints; }

    public void SetHitParticleEffects(GameObject defaultParticle, GameObject cbParticle) {
        hitParticleEffect = defaultParticle;
        hitParticleEffectColorBlind = cbParticle;
    }

    public void ModifyHealthPoints(int value, BulletType bulletType) {
        if (tag == "Boss" || tag == "Enemy") {
            if (HandleShield(bulletType)) { return; }
        }
        // TODO if adding health power-ups, will need to check if value is positive and ignore CameraShake if that's the case
        ShakeCamera();
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + value, 0, totalHealthPoints);
        audioSource.Play();
        if (SettingsManager.GetColorSet() == 0) {
            GameObject particles = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
            Destroy(particles, 0.5f);
        } else {
            GameObject particles = Instantiate(hitParticleEffectColorBlind, transform.position, Quaternion.identity);
            Destroy(particles, 0.5f);
        }
        if (currentHealthPoints == 0) {
            Death();
        }
    }

    private void Death() {
        Instantiate(deathSoundEffectObject, transform.position, Quaternion.identity);
        if (tag == "Player") {
            StartCoroutine(TriggerDeathTransition());
        } else if (tag == "Boss") {
            Boss boss = GetComponent<Boss>();
            boss.AddScoreToPlayer();
            boss.DeathAnimation();
        } else {
            GetComponent<Enemy>().AddScoreToPlayer();
            Destroy(gameObject);
        }
        if (SettingsManager.GetColorSet() == 0) {
            GameObject particles = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
            Destroy(particles, 0.5f);
        } else {
            GameObject particles = Instantiate(hitParticleEffectColorBlind, transform.position, Quaternion.identity);
            Destroy(particles, 0.5f);
        }
    }

    private bool HandleShield(BulletType bulletType) {
        if (shieldObject == null || !shieldObject.activeInHierarchy) {
            return false;
        } else if (shieldWeakness != BulletType.None && shieldWeakness != bulletType) {
            return true;
        } else if (shieldObject.activeInHierarchy && shieldWeakness == bulletType) {
            shieldObject.SetActive(false);
            brokenShieldObject.SetActive(true);
            return true;
        }
        Debug.Log("No statements in if chain apply to " + gameObject.name + ", returning false");
        return false;
    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private void ShakeCamera() {
        if (cameraShake != null && applyCameraShake) {
            cameraShake.Play();
        }
    }

    private IEnumerator TriggerDeathTransition() {
        movement.enabled = false;
        spriteRenderer.color = new Color(0,0,0,0);
        playerParticleSystem.Stop();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadGameOver();
    }
}
