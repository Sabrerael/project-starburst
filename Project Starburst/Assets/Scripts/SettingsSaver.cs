using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSaver : MonoBehaviour {
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundFxVolumeSlider;
    [SerializeField] TMP_Dropdown colorDropdown1;
    [SerializeField] TMP_Dropdown colorDropdown2;
    [SerializeField] TMP_Dropdown colorDropdown3;
    [SerializeField] TMP_Dropdown colorDropdown4;
    [SerializeField] Image color1Example;
    [SerializeField] Image color2Example;
    [SerializeField] Image color3Example;
    [SerializeField] Image color4Example;
    [SerializeField] List<Material> projectileMaterials;
    [SerializeField] List<Material> shieldMaterials;
    [SerializeField] List<Material> particleMaterials;
    [SerializeField] List<ParticleSystem> particleSystems;

    [SerializeField] List<Color> colors;
    [SerializeField] List<Color> brighterColors;
    //[SerializeField] Toggle brightnessToggle1;
    //[SerializeField] Toggle brightnessToggle2;

    [SerializeField] float musicVolumeMaxValue = 0.325f;
    [SerializeField] float soundFxMaxVolume = 1f;

    private void Start() {
        musicVolumeSlider.value = SettingsManager.GetMusicVolume() / musicVolumeMaxValue;
        soundFxVolumeSlider.value = SettingsManager.GetSoundEffectsVolume() / soundFxMaxVolume;
        colorDropdown1.value = SettingsManager.GetColor1();
        color1Example.material.SetColor("_ReplacedColor", colors[colorDropdown1.value]);
        projectileMaterials[0].SetColor("_Color", brighterColors[colorDropdown1.value] * 6);
        shieldMaterials[0].SetColor("_Color", brighterColors[colorDropdown1.value] * 6);
        particleMaterials[0].SetColor("_BaseColor", brighterColors[colorDropdown1.value]);
        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdown1.value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        var col = particleSystems[0].colorOverLifetime;
        col.color = grad;

        colorDropdown2.value = SettingsManager.GetColor2();
        color2Example.material.SetColor("_ReplacedColor", colors[colorDropdown2.value]);
        projectileMaterials[1].SetColor("_Color", brighterColors[colorDropdown2.value] * 6);
        shieldMaterials[1].SetColor("_Color", brighterColors[colorDropdown2.value] * 6);
        particleMaterials[1].SetColor("_BaseColor", brighterColors[colorDropdown2.value]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdown2.value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[1].colorOverLifetime;
        col.color = grad;

        colorDropdown3.value = SettingsManager.GetColor3();
        color3Example.material.SetColor("_ReplacedColor", colors[colorDropdown3.value]);
        projectileMaterials[2].SetColor("_Color", brighterColors[colorDropdown3.value] * 6);
        shieldMaterials[2].SetColor("_Color", brighterColors[colorDropdown3.value] * 6);
        particleMaterials[2].SetColor("_BaseColor", brighterColors[colorDropdown3.value]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdown3.value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[2].colorOverLifetime;
        col.color = grad;

        colorDropdown4.value = SettingsManager.GetColor4();
        color4Example.material.SetColor("_ReplacedColor", colors[colorDropdown4.value]);
        projectileMaterials[3].SetColor("_Color", brighterColors[colorDropdown4.value] * 6);
        shieldMaterials[3].SetColor("_Color", brighterColors[colorDropdown4.value] * 6);
        particleMaterials[3].SetColor("_BaseColor", brighterColors[colorDropdown4.value]);
        grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[colorDropdown4.value], 0.0f), 
                                               new GradientColorKey(Color.white, 1.0f) }, 
                                               new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                               new GradientAlphaKey(0.0f, 1.0f) } );
        col = particleSystems[3].colorOverLifetime;
        col.color = grad;
    }

    public void ResetToDefaultValues() {
        musicVolumeSlider.value = 1;
        soundFxVolumeSlider.value = 1;
        colorDropdown1.value = 0;
        color1Example.material.SetColor("_ReplacedColor", colors[0]);
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

        colorDropdown2.value = 1;
        color2Example.material.SetColor("_ReplacedColor", colors[1]);
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

        colorDropdown3.value = 2;
        color3Example.material.SetColor("_ReplacedColor", colors[2]);
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

        colorDropdown4.value = 3;
        color4Example.material.SetColor("_ReplacedColor", colors[3]);
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
    }

    public void SaveButton() {
        //if (brightnessToggle2.isOn) {
        //    activeBrightnessToggleValue = 1;
        //}

        float musicVolume = musicVolumeSlider.value * musicVolumeMaxValue;
        float soundEffectsVolume = soundFxVolumeSlider.value * soundFxMaxVolume;

        SettingsManager.SaveAllSettings(colorDropdown1.value,
                                        colorDropdown2.value,
                                        colorDropdown3.value,
                                        colorDropdown4.value,
                                        musicVolume,
                                        soundEffectsVolume);
        levelLoader.LoadMainMenu();
    }

    public void SetExampleColor(int value) {
        // TODO Reorder value to prevent setting the same color for mulitple ships
        if (value == 0) {
            int dropdownValue = colorDropdown1.value;
            color1Example.material.SetColor("_ReplacedColor", colors[dropdownValue]);
            projectileMaterials[0].SetColor("_Color", brighterColors[dropdownValue] * 6);
            shieldMaterials[0].SetColor("_Color", brighterColors[dropdownValue] * 6);
            particleMaterials[0].SetColor("_BaseColor", colors[dropdownValue]);
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[dropdownValue], 0.0f), 
                                                   new GradientColorKey(Color.white, 1.0f) }, 
                          new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                                   new GradientAlphaKey(0.0f, 1.0f) } );
            var col = particleSystems[0].colorOverLifetime;
            col.color = grad;
        } else if (value == 1) {
            int dropdownValue = colorDropdown2.value;
            color2Example.material.SetColor("_ReplacedColor", colors[dropdownValue]);
            projectileMaterials[1].SetColor("_Color", brighterColors[dropdownValue] * 6);
            shieldMaterials[1].SetColor("_Color", brighterColors[dropdownValue] * 6);
            particleMaterials[1].SetColor("_BaseColor", colors[dropdownValue]);
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[dropdownValue], 0.0f), 
                                                   new GradientColorKey(Color.white, 1.0f) }, 
                          new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                                   new GradientAlphaKey(0.0f, 1.0f) } );
            var col = particleSystems[1].colorOverLifetime;
            col.color = grad;
        } else if (value == 2) {
            int dropdownValue = colorDropdown3.value;
            color3Example.material.SetColor("_ReplacedColor", colors[dropdownValue]);
            projectileMaterials[2].SetColor("_Color", brighterColors[dropdownValue] * 6);
            shieldMaterials[2].SetColor("_Color", brighterColors[dropdownValue] * 6);
            particleMaterials[2].SetColor("_BaseColor", colors[dropdownValue]);
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[dropdownValue], 0.0f), 
                                                   new GradientColorKey(Color.white, 1.0f) }, 
                          new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                                   new GradientAlphaKey(0.0f, 1.0f) } );
            var col = particleSystems[2].colorOverLifetime;
            col.color = grad;
        } if (value == 3) {
            int dropdownValue = colorDropdown4.value;
            color4Example.material.SetColor("_ReplacedColor", colors[dropdownValue]);
            projectileMaterials[3].SetColor("_Color", brighterColors[dropdownValue] * 6);
            shieldMaterials[3].SetColor("_Color", brighterColors[dropdownValue] * 6);
            particleMaterials[3].SetColor("_BaseColor", colors[dropdownValue]);
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new GradientColorKey(brighterColors[dropdownValue], 0.0f), 
                                                   new GradientColorKey(Color.white, 1.0f) }, 
                          new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), 
                                                   new GradientAlphaKey(0.0f, 1.0f) } );
            var col = particleSystems[3].colorOverLifetime;
            col.color = grad;
        }
    }
}
