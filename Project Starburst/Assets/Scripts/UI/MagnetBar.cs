using UnityEngine;

public class MagnetBar : MonoBehaviour {
    [SerializeField] protected BulletMagnet bulletMagnet = null;
    [SerializeField] protected RectTransform foreground = null;
    [SerializeField] protected Canvas rootCanvas = null;

    protected virtual void Update() {
        rootCanvas.enabled = true;

        foreground.localScale = new Vector3(bulletMagnet.GetMagnetPower(), 1, 1);
    }
}
