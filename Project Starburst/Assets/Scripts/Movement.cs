using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float movementSpeed = 6;
    [SerializeField] float velocityAdjustment = 50000;

    private Vector2 movementValues = new Vector2();
    private Rigidbody2D playerRigidbody;

    private void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
    } 

    private void FixedUpdate() {
        if (movementValues.magnitude > Mathf.Epsilon) {
            NormalMovement();
        }
    }

    public void SetMovementValues(Vector2 values) {
        movementValues = values;
    }

    public void NormalMovement() {
        playerRigidbody.AddForce(new Vector2(movementValues.x * movementSpeed*velocityAdjustment * Time.fixedDeltaTime,
                                       movementValues.y * movementSpeed*velocityAdjustment * Time.fixedDeltaTime));
    }
}
