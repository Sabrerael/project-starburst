using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] BulletMagnet bulletMagnet;
    [SerializeField] GameObject pauseMenu;

    // CACHE
    private Movement movement;
    private bool isPaused;

    private void Start() {
        movement = GetComponent<Movement>();
    }

    private void OnFire(InputValue value) {
        if (value.isPressed && !isPaused) {
            bulletMagnet.LaunchBullet();
        }
    }

    private void OnMagnet(InputValue value) {
        if (value.isPressed && !isPaused) {
            bulletMagnet.SetMagnetActive(true);
        } else if (!value.isPressed && !isPaused) {
            bulletMagnet.SetMagnetActive(false);
        }
    }

    private void OnMove(InputValue value) {
        if (isPaused) { return; }
        movement.SetMovementValues(value.Get<Vector2>());
    }

    private void OnPause(InputValue value) {
        if (pauseMenu == null) {
            pauseMenu = GameObject.FindObjectOfType<PauseMenu>(true).gameObject;
            Time.timeScale = 0;
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            SetIsPaused(true);
        } else if (!pauseMenu.activeInHierarchy) {
            Time.timeScale = 0;
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            SetIsPaused(true);
        }
    }

    public void SetIsPaused(bool isPaused) { this.isPaused = isPaused; }
}
