using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static PauseMenu instance = null;

    private PlayerController playerController;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void ClosePauseMenu() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        playerController.SetIsPaused(false);
    }
}
