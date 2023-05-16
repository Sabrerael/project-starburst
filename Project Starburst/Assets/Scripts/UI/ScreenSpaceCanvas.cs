using UnityEngine;

public class ScreenSpaceCanvas : MonoBehaviour {
    private void Update() {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas.worldCamera == null) {
            canvas.worldCamera = Camera.main;
        }
    }
}
