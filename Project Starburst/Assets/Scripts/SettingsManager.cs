using UnityEngine;

public class SettingsManager : MonoBehaviour {
    // Variables

    private const string BRIGHTNESS_LEVEL = "brightnessLevel";
    private const string COLOR_SET = "colorSet";
    private const string MUSIC_VOLUME = "musicVolume";
    private const string SOUND_EFFECTS_VOLUME = "soundEffectsVolume";

    private static int brightnessLevel;
    private static int colorSet;
    private static float musicVolume;
    private static float soundEffectsVolume;

    public static SettingsManager instance = null;

    // Unity functions

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        SetupSettings();
    }

    // Getters

    public static int GetBrightnessLevel() { return brightnessLevel; }
    public static int GetColorSet() { return colorSet; }
    public static float GetMusicVolume() { return musicVolume; }
    public static float GetSoundEffectsVolume() { return soundEffectsVolume; }

    // Setters

    public static void SetBrightnessLevel(int _brightnessLevel) {
        PlayerPrefs.SetInt(BRIGHTNESS_LEVEL, _brightnessLevel);
        brightnessLevel = _brightnessLevel;
    }

    public static void SetColorSet(int _colorSet) { 
        PlayerPrefs.SetInt(COLOR_SET, _colorSet);
        colorSet = _colorSet;
    }

    public static void SetMusicVolume(float _musicVolume) { 
        PlayerPrefs.SetFloat(MUSIC_VOLUME, _musicVolume);
        musicVolume = _musicVolume;
    }

    public static void SetSoundEffectsVolume(float _soundEffectsVolume) { 
        PlayerPrefs.SetFloat(SOUND_EFFECTS_VOLUME, _soundEffectsVolume);
        soundEffectsVolume = _soundEffectsVolume;
    }

    // Public Functions

    public static void SaveAllSettings(int brightnessLevel, int colorSet, float musicVolume, float soundEffectsVolume) {
        SetBrightnessLevel(brightnessLevel);
        SetColorSet(colorSet);
        SetMusicVolume(musicVolume);
        SetSoundEffectsVolume(soundEffectsVolume);
    }

    // Private Functions
    private void SetupSettings() {
        brightnessLevel = PlayerPrefs.GetInt(BRIGHTNESS_LEVEL, 0);
        colorSet = PlayerPrefs.GetInt(COLOR_SET, 0);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.1f);
        soundEffectsVolume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME, 1f);
    }
}
