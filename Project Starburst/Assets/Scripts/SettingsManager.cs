using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    // Variables

    private const string BRIGHTNESS_LEVEL = "brightnessLevel";
    private const string COLOR_1 = "color1";
    private const string COLOR_2 = "color2";
    private const string COLOR_3 = "color3";
    private const string COLOR_4 = "color4";
    private const string MUSIC_VOLUME = "musicVolume";
    private const string SOUND_EFFECTS_VOLUME = "soundEffectsVolume";

    private static int brightnessLevel;
    private static int color1;
    private static int color2;
    private static int color3;
    private static int color4;
    private static float musicVolume;
    private static float soundEffectsVolume;
    
    public static event Action onSettingsChange;
    public static SettingsManager instance = null;

    // Unity functions

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SetupSettings();
    }

    // Getters

    public static int GetBrightnessLevel() { return brightnessLevel; }
    public static int GetColor1() { return color1; }
    public static int GetColor2() { return color2; }
    public static int GetColor3() { return color3; }
    public static int GetColor4() { return color4; }
    public static float GetMusicVolume() { return musicVolume; }
    public static float GetSoundEffectsVolume() { return soundEffectsVolume; }

    // Setters

    public static void SetBrightnessLevel(int _brightnessLevel) {
        PlayerPrefs.SetInt(BRIGHTNESS_LEVEL, _brightnessLevel);
        brightnessLevel = _brightnessLevel;
    }

    public static void SetColor1(int _color1) { 
        PlayerPrefs.SetInt(COLOR_1, _color1);
        color1 = _color1;
    }

    public static void SetColor2(int _color2) { 
        PlayerPrefs.SetInt(COLOR_2, _color2);
        color2 = _color2;
    }

    public static void SetColor3(int _color3) { 
        PlayerPrefs.SetInt(COLOR_3, _color3);
        color3 = _color3;
    }

    public static void SetColor4(int _color4) { 
        PlayerPrefs.SetInt(COLOR_4, _color4);
        color4 = _color4;
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

    public static void SaveAllSettings(int color1, int color2, int color3, int color4,
                                       float musicVolume, float soundEffectsVolume) {
        //SetBrightnessLevel(brightnessLevel);
        SetColor1(color1);
        SetColor2(color2);
        SetColor3(color3);
        SetColor4(color4);
        SetMusicVolume(musicVolume);
        SetSoundEffectsVolume(soundEffectsVolume);
        if (onSettingsChange != null) { onSettingsChange(); }
    }

    // Private Functions

    private void SetupSettings() {
        //brightnessLevel = PlayerPrefs.GetInt(BRIGHTNESS_LEVEL, 0);
        color1 = PlayerPrefs.GetInt(COLOR_1, 0);
        color2 = PlayerPrefs.GetInt(COLOR_2, 1);
        color3 = PlayerPrefs.GetInt(COLOR_3, 2);
        color4 = PlayerPrefs.GetInt(COLOR_4, 3);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.325f);
        soundEffectsVolume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME, 1f);
    }
}
