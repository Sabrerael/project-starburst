using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    // CACHE
    private BulletMagnet bulletMagnet;
    private Movement movement;

    private void Start() {
        bulletMagnet = GetComponent<BulletMagnet>();
        movement = GetComponent<Movement>();
    }

    private void OnFire(InputValue value) {
        if (value.isPressed) {
            bulletMagnet.LaunchBullet();
        }
    }

    private void OnMove(InputValue value) {
        movement.SetMovementValues(value.Get<Vector2>());
    }

}
