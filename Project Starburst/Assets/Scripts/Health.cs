using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;
    [SerializeField] GameObject deathSoundEffectObject;

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

    public void ModifyHealthPoints(int value) {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + value, 0, totalHealthPoints);
        audioSource.Play();
        if (currentHealthPoints == 0) {
            Death();
        }
    }

    private void Death() {
        Instantiate(deathSoundEffectObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
        if (tag == "Player") {
            StartCoroutine(TriggerDeathTransition());
        } else if (tag == "Boss") {
            GetComponent<Boss>().AddScoreToPlayer();
            StartCoroutine(TriggerWinTransition());
        } else {
            GetComponent<Enemy>().AddScoreToPlayer();
        }

    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private IEnumerator TriggerDeathTransition() {
        yield return new WaitForSeconds(1);
        FindObjectOfType<LevelLoader>().LoadGameOver();
    }

    private IEnumerator TriggerWinTransition() {
        yield return new WaitForSeconds(1);
        FindObjectOfType<LevelLoader>().LoadWinScreen();
    }
}
