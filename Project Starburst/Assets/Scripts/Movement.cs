using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float movementSpeed = 6;
    [SerializeField] float xBoundary;
    [SerializeField] float yBoundary;

    private Vector2 movementValues = new Vector2();
    private Rigidbody2D playerRigidbody;

    private void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
    } 

    private void Update() {
        if (movementValues.magnitude > Mathf.Epsilon) {
            NormalMovement();
        }
    }

    public void SetMovementValues(Vector2 values) {
        movementValues = values;
    }

    public void NormalMovement() {
        Vector2 delta = movementValues * movementSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, -xBoundary, xBoundary);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, -yBoundary, yBoundary - 1f);
        transform.position = newPosition;
    }
}
