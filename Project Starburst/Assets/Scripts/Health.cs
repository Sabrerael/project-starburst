using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;
    [SerializeField] GameObject deathSoundEffectObject;
    [SerializeField] GameObject hitParticleEffect;
    [SerializeField] GameObject hitParticleEffectColorBlind;

    [Header("Player Specific Properties")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem playerParticleSystem;

    private Animator animator;
    private AudioSource audioSource;
    private Movement movement;
    private int totalHealthPoints;
    private int currentHealthPoints;

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

    public void ModifyHealthPoints(int value) {
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
            GetComponent<Boss>().AddScoreToPlayer();
            StartCoroutine(TriggerWinTransition());
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

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private IEnumerator TriggerDeathTransition() {
        movement.enabled = false;
        spriteRenderer.color = new Color(0,0,0,0);
        playerParticleSystem.Stop();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadGameOver();
    }

    private IEnumerator TriggerWinTransition() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadWinScreen();
    }
}
