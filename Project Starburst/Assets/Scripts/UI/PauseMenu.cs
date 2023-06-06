using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    public static PauseMenu instance = null;

    [SerializeField] GameObject pauseFirstButton, optionsFirstObject, optionsClosedButton;

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

    public void SetButtonActive() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void SetOptionsMenuActive() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstObject);
    }

    public void SetOptionsMenuInactive() {
        Debug.Log("SetOptionsMenuInactive function hit");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }
}
