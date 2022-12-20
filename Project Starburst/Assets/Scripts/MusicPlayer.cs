using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public static MusicPlayer instance = null;

    private AudioSource audioSource;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsManager.GetMusicVolume();
    }
}