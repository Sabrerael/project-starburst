using UnityEngine;

public class EndOfLevelPlayerClone : MonoBehaviour {
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
    }

    public void PlaySoundEffect() {
        audioSource.Play();
    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
