using UnityEngine;

public class HUD : MonoBehaviour {
    public static HUD instance = null;

    [SerializeField] Canvas canvas;

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

    private void Update() {
        if (canvas.worldCamera == null) {
            canvas.worldCamera = Camera.main;
        }
    }
}
