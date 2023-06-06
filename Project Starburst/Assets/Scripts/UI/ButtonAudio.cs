using UnityEngine;

public class ButtonAudio : MonoBehaviour {
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
    }

    private void OnDestroy() {
        SettingsManager.onSettingsChange -= SetVolume;
    }

    public void PlayAudio() {
        Debug.Log("PlayAudio function hit");
        audioSource.Play();
    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
