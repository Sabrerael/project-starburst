using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    private Vector3 initialPosition;

    public static CameraShake instance = null;

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

    private void Start() {
        initialPosition = transform.position;
    }

    public void Play() {
        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera() {
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration) {
            transform.position = initialPosition + (Vector3)(Random.insideUnitCircle * shakeMagnitude);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
