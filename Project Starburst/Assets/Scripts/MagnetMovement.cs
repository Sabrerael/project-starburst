using UnityEngine;

public class MagnetMovement : MonoBehaviour {
    [SerializeField] float lerpTime = 0.15f;

    private bool lerping = false;
    private float timer = 0;
    private Vector3 initialLocation;
    private Vector3 movementLocation;

    private void Update() {
        if (lerping) {
            Debug.Log("Lerping");
            timer += .05f;
            transform.localPosition = Vector3.Lerp(initialLocation, movementLocation, timer);
            if (transform.localPosition == movementLocation) {
                lerping = false;
            }
        }
    }

    public void SetMovementLocation(Vector3 movementLocation) {
        Debug.Log("In SetMovementLocation");
        initialLocation = transform.localPosition;
        this.movementLocation = movementLocation;
        timer = 0;
        lerping = true;
    }
}
