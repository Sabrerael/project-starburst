using UnityEngine;

public class TemporarySoundEffect : MonoBehaviour {
    [SerializeField] float lifetime = 1f;

    private AudioSource audioSource;
    private float timer = 0;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= lifetime) { Destroy(gameObject);}
    }

    private void OnDestroy() {
        SettingsManager.onSettingsChange -= SetVolume;
    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
