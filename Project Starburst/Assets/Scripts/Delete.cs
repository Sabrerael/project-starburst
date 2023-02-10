using UnityEngine;

public class Delete : MonoBehaviour {
    [SerializeField] float deleteTime = 0.5f;

    private float timer = 0;
    private Material material;

    private void Start() {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update() {
        timer += Time.deltaTime;
        float alphaValue = material.GetFloat("_Alpha");
        float newAlphaValue = Mathf.Max(alphaValue - (Time.deltaTime / deleteTime), 0);
        material.SetFloat("_Alpha", newAlphaValue);
        if (timer >= deleteTime) { Destroy(gameObject); }
    }
}
