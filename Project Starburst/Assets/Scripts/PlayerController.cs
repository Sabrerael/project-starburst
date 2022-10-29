using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] BulletMagnet bulletMagnet;

    // CACHE
    private Movement movement;

    private void Start() {
        movement = GetComponent<Movement>();
    }

    private void OnFire(InputValue value) {
        if (value.isPressed) {
            bulletMagnet.LaunchBullet();
        }
    }

    private void OnMagnet(InputValue value) {
        if (value.isPressed) {
            bulletMagnet.SetMagnetActive(true);
        } else if (!value.isPressed) {
            bulletMagnet.SetMagnetActive(false);
        }
    }

    private void OnMove(InputValue value) {
        movement.SetMovementValues(value.Get<Vector2>());
    }
}
