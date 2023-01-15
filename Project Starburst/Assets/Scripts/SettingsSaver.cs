using UnityEngine;
using UnityEngine.UI;

public class SettingsSaver : MonoBehaviour {
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundFxVolumeSlider;
    [SerializeField] Toggle colorSchemeToggle1;
    [SerializeField] Toggle colorSchemeToggle2;
    //[SerializeField] Toggle brightnessToggle1;
    //[SerializeField] Toggle brightnessToggle2;

    [SerializeField] float musicVolumeMaxValue = 0.325f;
    [SerializeField] float soundFxMaxVolume = 1f;

    private void Start() {
        int colorSet = SettingsManager.GetColorSet();
        if (colorSet == 0) {
            colorSchemeToggle1.isOn = true;
        } else {
            colorSchemeToggle2.isOn = true;
        }
        musicVolumeSlider.value = SettingsManager.GetMusicVolume() / musicVolumeMaxValue;
        soundFxVolumeSlider.value = SettingsManager.GetSoundEffectsVolume() / soundFxMaxVolume;
    }

    public void SaveButton() {
        int activeBrightnessToggleValue = 0;
        //if (brightnessToggle2.isOn) {
        //    activeBrightnessToggleValue = 1;
        //}

        int activeColorToggleValue = 0;
        if (colorSchemeToggle2.isOn) {
            activeColorToggleValue = 1;
        }

        float musicVolume = musicVolumeSlider.value * musicVolumeMaxValue;
        float soundEffectsVolume = soundFxVolumeSlider.value * soundFxMaxVolume;

        SettingsManager.SaveAllSettings(activeBrightnessToggleValue,
                                        activeColorToggleValue,
                                        musicVolume,
                                        soundEffectsVolume);
        levelLoader.LoadMainMenu();
    }
}
