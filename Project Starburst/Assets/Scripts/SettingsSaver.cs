using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSaver : MonoBehaviour {
    [SerializeField] bool inPauseMenu = false;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] GameObject optionCanvas;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundFxVolumeSlider;
    [SerializeField] List<TMP_Dropdown> colorDropdowns;
    [SerializeField] List<Image> colorExamples;
    [SerializeField] Button saveButton;
    [SerializeField] GameObject warningText;
    [SerializeField] List<Material> projectileMaterials;
    [SerializeField] List<Material> shieldMaterials;
    [SerializeField] List<Material> particleMaterials;
    [SerializeField] List<ParticleSystem> particleSystems;
    [SerializeField] List<ParticleSystem> projectileParticleSystems;

    // These might be need to be moved or duplicated into the SettingsManager.
    [SerializeField] List<Color> colors;
    [SerializeField] List<Color> brighterColors;

    [SerializeField] float musicVolumeMaxValue = 0.325f;
    [SerializeField] float soundFxMaxVolume = 1f;

    private void Start() {
        musicVolumeSlider.value = SettingsManager.GetMusicVolume() / musicVolumeMaxValue;
        soundFxVolumeSlider.value = SettingsManager.GetSoundEffectsVolume() / soundFxMaxVolume;
        colorDropdowns[0].value = SettingsManager.GetColor1();
        colorExamples[0].material.SetColor("_ReplacedColor", colors[colorDropdowns[0].value]);
        projectileMaterials[0].SetColor("_Color", brighterColors[colorDropdowns[0].value] * 6);
        shieldMaterials[0].SetColor("_Color", brighterColors[colorDropdowns[0].value] * 6);
        particleMaterials[0].SetColor("_BaseColor", brighterColors[colorDropdowns[0].value]);
        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdowns[0].value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        var col = particleSystems[0].colorOverLifetime;
        col.color = grad;
        col = projectileParticleSystems[0].colorOverLifetime;
        col.color = grad;

        colorDropdowns[1].value = SettingsManager.GetColor2();
        colorExamples[1].material.SetColor("_ReplacedColor", colors[colorDropdowns[1].value]);
        projectileMaterials[1].SetColor("_Color", brighterColors[colorDropdowns[1].value] * 6);
        shieldMaterials[1].SetColor("_Color", brighterColors[colorDropdowns[1].value] * 6);
        particleMaterials[1].SetColor("_BaseColor", brighterColors[colorDropdowns[1].value]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdowns[1].value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[1].colorOverLifetime;
        col.color = grad;

        colorDropdowns[2].value = SettingsManager.GetColor3();
        colorExamples[2].material.SetColor("_ReplacedColor", colors[colorDropdowns[2].value]);
        projectileMaterials[2].SetColor("_Color", brighterColors[colorDropdowns[2].value] * 6);
        shieldMaterials[2].SetColor("_Color", brighterColors[colorDropdowns[2].value] * 6);
        particleMaterials[2].SetColor("_BaseColor", brighterColors[colorDropdowns[2].value]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdowns[2].value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[2].colorOverLifetime;
        col.color = grad;
        col = projectileParticleSystems[2].colorOverLifetime;
        col.color = grad;

        colorDropdowns[3].value = SettingsManager.GetColor4();
        colorExamples[3].material.SetColor("_ReplacedColor", colors[colorDropdowns[3].value]);
        projectileMaterials[3].SetColor("_Color", brighterColors[colorDropdowns[3].value] * 6);
        shieldMaterials[3].SetColor("_Color", brighterColors[colorDropdowns[3].value] * 6);
        particleMaterials[3].SetColor("_BaseColor", brighterColors[colorDropdowns[3].value]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdowns[3].value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[3].colorOverLifetime;
        col.color = grad;
        col = projectileParticleSystems[3].colorOverLifetime;
        col.color = grad;
    }

    public Color GetSpreadshotColor() {
        return colors[PlayerPrefs.GetInt("color4")];
    }

    public Color GetMissileColor(int color2Index) {
        return colors[color2Index];
    }

    public Color GetSpreadshotColor(int color4Index) {
        return colors[color4Index];
    }

    public Color GetTrackingColor(int color5Index) {
        return colors[color5Index];
    }

    public void ResetToDefaultValues() {
        musicVolumeSlider.value = 1;
        soundFxVolumeSlider.value = 1;
        colorDropdowns[0].value = 0;
        colorExamples[0].material.SetColor("_ReplacedColor", colors[0]);
        projectileMaterials[0].SetColor("_Color", brighterColors[0] * 6);
        shieldMaterials[0].SetColor("_Color", brighterColors[0] * 6);
        particleMaterials[0].SetColor("_BaseColor", brighterColors[0]);
        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[0], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        var col = particleSystems[0].colorOverLifetime;
        col.color = grad;
        col = projectileParticleSystems[0].colorOverLifetime;
        col.color = grad;

        colorDropdowns[1].value = 1;
        colorExamples[1].material.SetColor("_ReplacedColor", colors[1]);
        projectileMaterials[1].SetColor("_Color", brighterColors[1] * 6);
        shieldMaterials[1].SetColor("_Color", brighterColors[1] * 6);
        particleMaterials[1].SetColor("_BaseColor", brighterColors[1]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[1], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[1].colorOverLifetime;
        col.color = grad;

        colorDropdowns[2].value = 2;
        colorExamples[2].material.SetColor("_ReplacedColor", colors[2]);
        projectileMaterials[2].SetColor("_Color", brighterColors[2] * 6);
        shieldMaterials[2].SetColor("_Color", brighterColors[2] * 6);
        particleMaterials[2].SetColor("_BaseColor", brighterColors[2]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[2], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[2].colorOverLifetime;
        col.color = grad;
        col = projectileParticleSystems[2].colorOverLifetime;
        col.color = grad;

        colorDropdowns[3].value = 3;
        colorExamples[3].material.SetColor("_ReplacedColor", colors[3]);
        projectileMaterials[3].SetColor("_Color", brighterColors[3] * 6);
        shieldMaterials[3].SetColor("_Color", brighterColors[3] * 6);
        particleMaterials[3].SetColor("_BaseColor", brighterColors[3]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[3], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[3].colorOverLifetime;
        col.color = grad;
        col = projectileParticleSystems[3].colorOverLifetime;
        col.color = grad;
    }

    public void SaveButton() {
        //if (brightnessToggle2.isOn) {
        //    activeBrightnessToggleValue = 1;
        //}
        Debug.Log("SaveButton Function hit");

        float musicVolume = musicVolumeSlider.value * musicVolumeMaxValue;
        float soundEffectsVolume = soundFxVolumeSlider.value * soundFxMaxVolume;

        SettingsManager.SaveAllSettings(colorDropdowns[0].value,
                                        colorDropdowns[1].value,
                                        colorDropdowns[2].value,
                                        colorDropdowns[3].value,
                                        5, 6, 7,
                                        musicVolume,
                                        soundEffectsVolume);
        SettingsManager.SetLevel2BossColor(colors[colorDropdowns[3].value]);
        if (inPauseMenu) {
            optionCanvas.SetActive(false);
        } else {
            levelLoader.LoadMainMenu();
        }
    }

    public void SetExampleColor(int value) {
        int dropdownValue = colorDropdowns[value].value;
        colorExamples[value].material.SetColor("_ReplacedColor", colors[dropdownValue]);
        projectileMaterials[value].SetColor("_Color", brighterColors[dropdownValue] * 6);
        shieldMaterials[value].SetColor("_Color", brighterColors[dropdownValue] * 6);
        particleMaterials[value].SetColor("_BaseColor", colors[dropdownValue]);
        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[dropdownValue], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                      new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        var col = particleSystems[value].colorOverLifetime;
        col.color = grad;
        col = projectileParticleSystems[value].colorOverLifetime;
        col.color = grad;
        
        UserEntryCheck();
    }

    private void UserEntryCheck() {
        if (CheckForMatchingColors()) {
            saveButton.interactable = false;
            warningText.SetActive(true);
        } else {
            saveButton.interactable = true;
            warningText.SetActive(false);
        }

    }

    private bool CheckForMatchingColors() {
        for (int i = 0; i < colorDropdowns.Count; i++) {
            for (int j = i+1; j < colorDropdowns.Count; j++) {
                if (colorDropdowns[i].value == colorDropdowns[j].value) { return true; }
            }
        }
        return false;
    }
}
