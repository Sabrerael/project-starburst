using Steamworks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class SettingsManager : MonoBehaviour {
    // Variables

    private static Color level2BossColor;

    private const string BRIGHTNESS_LEVEL = "brightnessLevel";
    private const string COLOR_1 = "color1";
    private const string COLOR_2 = "color2";
    private const string COLOR_3 = "color3";
    private const string COLOR_4 = "color4";
    private const string COLOR_5 = "color5";
    private const string COLOR_6 = "color6";
    private const string COLOR_7 = "color7";
    private const string MUSIC_VOLUME = "musicVolume";
    private const string SOUND_EFFECTS_VOLUME = "soundEffectsVolume";

    private static int brightnessLevel;
    private static int color1;
    private static int color2;
    private static int color3;
    private static int color4;
    private static int color5;
    private static int color6;
    private static int color7;
    private static float musicVolume;
    private static float soundEffectsVolume;

    private static SettingsSaver settingsSaver;
    
    public static event Action onSettingsChange;
    public static SettingsManager instance = null;

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

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

    private void Start() {
        settingsSaver = FindObjectOfType<SettingsSaver>();
        if (settingsSaver != null) {
            level2BossColor = settingsSaver.GetSpreadshotColor();
        }
        InputUser.onChange += onInputDeviceChange;
    }

    private void OnEnable() {
		if (SteamManager.Initialized) {
			m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
		}
	}

    // Getters

    public static Color GetBasicColor() {
        if (settingsSaver != null) {
            return settingsSaver.GetBasicColor(color1);
        } else {
            settingsSaver = FindObjectOfType<SettingsSaver>();
            return settingsSaver.GetBasicColor(color1);
        }
    }

    public static Color GetLevel2BossColor() {
        if (settingsSaver != null) {
            return settingsSaver.GetSpreadshotColor(color4);
        } else {
            settingsSaver = FindObjectOfType<SettingsSaver>();
            return settingsSaver.GetSpreadshotColor(color4);
        }
    }

    public static Color GetMissileColor() {
        if (settingsSaver != null) {
            return settingsSaver.GetMissileColor(color2);
        } else {
            settingsSaver = FindObjectOfType<SettingsSaver>();
            return settingsSaver.GetMissileColor(color2);
        }
    }

    public static Color GetPiercingColor() {
        if (settingsSaver != null) {
            return settingsSaver.GetPiercingColor(color3);
        } else {
            settingsSaver = FindObjectOfType<SettingsSaver>();
            return settingsSaver.GetPiercingColor(color3);
        }
    }

    public static Color GetTrackingColor() {
        if (settingsSaver != null) {
            return settingsSaver.GetTrackingColor(color5);
        } else {
            settingsSaver = FindObjectOfType<SettingsSaver>();
            return settingsSaver.GetTrackingColor(color5);
        }
    }

    public static Color GetProxyMineColor() {
        if (settingsSaver != null) {
            return settingsSaver.GetProxyMineColor(color6);
        } else {
            settingsSaver = FindObjectOfType<SettingsSaver>();
            return settingsSaver.GetProxyMineColor(color6);
        }
    }

    public static Color GetBoomerangColor() {
        if (settingsSaver != null) {
            return settingsSaver.GetBoomerangColor(color7);
        } else {
            settingsSaver = FindObjectOfType<SettingsSaver>();
            return settingsSaver.GetBoomerangColor(color7);
        }
    }

    public static int GetBrightnessLevel() { return brightnessLevel; }
    public static int GetColor1() { return color1; }
    public static int GetColor2() { return color2; }
    public static int GetColor3() { return color3; }
    public static int GetColor4() { return color4; }
    public static int GetColor5() { return color5; }
    public static int GetColor6() { return color6; }
    public static int GetColor7() { return color7; }
    public static float GetMusicVolume() { return musicVolume; }
    public static float GetSoundEffectsVolume() { return soundEffectsVolume; }

    // Setters

    public static void SetLevel2BossColor(Color color) { 
        level2BossColor = color;
    }

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

    public static void SetColor5(int _color5) { 
        PlayerPrefs.SetInt(COLOR_5, _color5);
        color5 = _color5;
    }

    public static void SetColor6(int _color6) { 
        PlayerPrefs.SetInt(COLOR_6, _color6);
        color6 = _color6;
    }

    public static void SetColor7(int _color7) { 
        PlayerPrefs.SetInt(COLOR_7, _color7);
        color7 = _color7;
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
                                       int color5, int color6, int color7,
                                       float musicVolume, float soundEffectsVolume) {
        //SetBrightnessLevel(brightnessLevel);
        SetColor1(color1);
        SetColor2(color2);
        SetColor3(color3);
        SetColor4(color4);
        SetColor5(color5);
        SetColor6(color6);
        SetColor7(color7);
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
        color5 = PlayerPrefs.GetInt(COLOR_5, 4);
        color6 = PlayerPrefs.GetInt(COLOR_6, 5);
        color7 = PlayerPrefs.GetInt(COLOR_7, 6);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.325f);
        soundEffectsVolume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME, 1f);
    }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {
        if (change == InputUserChange.ControlSchemeChanged) {
            updateButtonImage(user.controlScheme.Value.name);
        } else if (change == InputUserChange.DeviceLost) {
            FindObjectOfType<PlayerController>().ForcePaused();
        }
    }
 
    void updateButtonImage(string schemeName) {
       // assuming you have only 2 schemes: keyboard and gamepad
        if (schemeName.Equals("Gamepad")) {
            Cursor.visible = false;
        }
        else {
            Cursor.visible = true;
        }
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback) {
		if(pCallback.m_bActive != 0) {
            FindObjectOfType<PlayerController>().ForcePaused();
		}
	}
}
