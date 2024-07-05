using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBreakAudio : MonoBehaviour {
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
        SettingsManager.onSettingsChange += SetVolume;
        
    }

    private void OnDestroy() {
        SettingsManager.onSettingsChange -= SetVolume;
    }

    private void SetVolume() {
        audioSource.volume = SettingsManager.GetSoundEffectsVolume();
    }
}
