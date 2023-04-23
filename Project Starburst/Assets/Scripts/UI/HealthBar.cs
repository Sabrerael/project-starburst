using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] protected Health healthComponent = null;
    [SerializeField] protected RectTransform foreground = null;
    [SerializeField] protected Canvas rootCanvas = null;

    private void Start() {
        if (healthComponent == null) {
            healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
    }

    protected virtual void Update() {
        rootCanvas.enabled = true;

        if (healthComponent != null) {
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        }
    }
}
