using UnityEngine;

public class MagnetMovement : MonoBehaviour {
    [SerializeField] float lerpTime = 0.15f;
    [SerializeField] float movementMagnitude = 0.05f;

    private bool lerping = false;
    private float timer = 0;
    private Vector3 initialLocation;
    private Vector3 movementLocation;
    private Vector3 stationaryLocation;

    private void Update() {
        if (lerping) {
            timer += lerpTime;
            transform.localPosition = Vector3.Lerp(initialLocation, movementLocation, timer);
            if (transform.localPosition == movementLocation) {
                lerping = false;
                stationaryLocation = transform.localPosition;
            }
        } else {
            transform.localPosition = stationaryLocation + (Vector3)(Random.insideUnitCircle * movementMagnitude);
        }
    }

    public void SetMovementLocation(Vector3 movementLocation) {
        initialLocation = transform.localPosition;
        this.movementLocation = movementLocation;
        timer = 0;
        lerping = true;
    }
}
