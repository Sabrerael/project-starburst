using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;
    [SerializeField] bool applyCameraShake = false;
    [SerializeField] GameObject deathSoundEffectObject;
    [SerializeField] GameObject hitParticleEffect;

    [Header("Enemy Specific Properties")]
    [SerializeField] BulletType weakness = BulletType.Basic;
    [SerializeField] BulletType shieldWeakness = BulletType.None;
    [SerializeField] GameObject shieldObject;
    [SerializeField] GameObject brokenShieldObject;

    [Header("Player Specific Properties")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem playerParticleSystem;
    [SerializeField] GameObject sparkParticles;
    [SerializeField] GameObject smokeParticles;
    [SerializeField] bool isEndless;
    [SerializeField] GameObject failureJingleObject;

    private Animator animator;
    private AudioSource audioSource;
    private CameraShake cameraShake;
    private Movement movement;
    private Player player;
    private int totalHealthPoints;
    private int currentHealthPoints;
    private bool isDead = false;

    private void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Start() {
        totalHealthPoints = healthPoints;
        currentHealthPoints = healthPoints;
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
    }

    private void OnDestroy() {
        SettingsManager.onSettingsChange -= SetVolume;
    }

    public int GetTotalHealthPoints() { return totalHealthPoints; }
    public int GetCurrentHealthPoints() { return currentHealthPoints; }
    public float GetFraction() { return (float)currentHealthPoints / (float)totalHealthPoints; }
    public void SetShieldObject(GameObject shieldObject) { this.shieldObject = shieldObject; }
    public void SetBrokenShieldObject(GameObject brokenShieldObject) { this.brokenShieldObject = brokenShieldObject; }
    public void SetShieldWeakness(BulletType shieldWeakness) { this.shieldWeakness = shieldWeakness; }

    public void SetHitParticleEffects(GameObject defaultParticle) {
        hitParticleEffect = defaultParticle;
    }

    public void ModifyHealthPoints(int value, BulletType bulletType) {
        if (tag == "Boss" || tag == "Enemy") {
            if (HandleShield(bulletType)) { return; }
            if (bulletType == weakness) { value *= 2; }
        } else if (tag == "Player") {
            player.ResetComboCounter();
        }
        ShakeCamera();
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + value, 0, totalHealthPoints);
        EnableParticles();
        audioSource.Play();

        if (currentHealthPoints == 0) {
            Death();
        }
    }

    public void ResetHealth() {
        currentHealthPoints = totalHealthPoints;
    }

    private void Death() {
        if (isDead) { return;} 

        isDead = true;

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

        GameObject particles = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
        Destroy(particles, 0.5f);
    }

    private void EnableParticles() {
        if (tag != "Player") { return; }

        if (currentHealthPoints <= totalHealthPoints * 0.66) {
            sparkParticles.SetActive(true);
        }

        if (currentHealthPoints <= totalHealthPoints / 3) {
            smokeParticles.SetActive(true);
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
        } else if (applyCameraShake) {
            cameraShake = Camera.main.GetComponent<CameraShake>();
            cameraShake.Play();
        }
    }

    private IEnumerator TriggerDeathTransition() {
        movement.enabled = false;
        spriteRenderer.color = new Color(0,0,0,0);
        sparkParticles.SetActive(false);
        smokeParticles.SetActive(false);
        playerParticleSystem.Stop();
        yield return new WaitForSeconds(0.5f);
        Instantiate(failureJingleObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        if (!isEndless) {
            FindObjectOfType<LevelLoader>().LoadGameOver();
        } else {
            FindObjectOfType<LevelLoader>().LoadGameOverEndlessMode();
        }
    }
}
