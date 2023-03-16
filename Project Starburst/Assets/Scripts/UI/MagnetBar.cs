using UnityEngine;

public class MagnetBar : MonoBehaviour {
    [SerializeField] protected BulletMagnet bulletMagnet = null;
    [SerializeField] protected RectTransform foreground = null;
    [SerializeField] protected Canvas rootCanvas = null;

    protected virtual void Update() {
        if (bulletMagnet.GetMagnetPower() < 1) {
            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(bulletMagnet.GetMagnetPower(), 1, 1);
        } else {
            rootCanvas.enabled = false;
            return;
        }
    }
}
