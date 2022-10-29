using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] protected Health healthComponent = null;
    [SerializeField] protected RectTransform foreground = null;
    [SerializeField] protected Canvas rootCanvas = null;

    protected virtual void Update() {
        rootCanvas.enabled = true;

        foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
    }
}
