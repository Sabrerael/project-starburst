using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;
    [SerializeField] GameObject deathSoundEffectObject;
    [SerializeField] GameObject hitParticleEffect;
    [SerializeField] GameObject hitParticleEffectColorBlind;

    private AudioSource audioSource;
    private int totalHealthPoints;
    private int currentHealthPoints;

    private void Start() {
        totalHealthPoints = healthPoints;
        currentHealthPoints = healthPoints;
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
        // Need to do this more discretely, maybe move the Coroutine into the LevelLoader?
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadGameOver();
    }

    private IEnumerator TriggerWinTransition() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadWinScreen();
    }
}
