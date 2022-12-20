using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int healthPoints = 100;
    [SerializeField] AudioClip deathSound;

    private AudioSource audioSource;
    private int totalHealthPoints;
    private int currentHealthPoints;

    private void Start() {
        totalHealthPoints = healthPoints;
        currentHealthPoints = healthPoints;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    public int GetTotalHealthPoints() { return totalHealthPoints; }
    public int GetCurrentHealthPoints() { return currentHealthPoints; }
    public float GetFraction() { return (float)currentHealthPoints / (float)totalHealthPoints; }

    public void ModifyHealthPoints(int value) {
        audioSource.Play();
        if (currentHealthPoints <= 0) {
            Death();
        }
    }

    private void Death() {
        audioSource.clip = deathSound;
        audioSource.Play();
        Destroy(gameObject);
        if (tag != "Player") {
            GetComponent<Enemy>().AddScoreToPlayer();
        }
    }
}
