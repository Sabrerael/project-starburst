using UnityEngine;

public class TemporarySoundEffect : MonoBehaviour {
    [SerializeField] float lifetime = 1f;

    private AudioSource audioSource;
    private float timer = 0;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= lifetime) { Destroy(gameObject);}
    }
}
