using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    // CACHE
    private Movement movement;

    private void Start() {
        movement = GetComponent<Movement>();
    }

    private void OnFire(InputValue value) {
        if (value.isPressed) {
            //GetComponent<Fighter>().CheckIfSwinging();
            Debug.Log("Attack");
        }
    }

    private void OnMove(InputValue value) {
        movement.SetMovementValues(value.Get<Vector2>());
    }

}
