using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField] float finalLength = 20.0f;
    [SerializeField] float extensionTime = 0.5f;

    SpriteRenderer spriteRenderer;
    BoxCollider2D lazerCollider;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(0.5f, finalLength);
        lazerCollider = GetComponent<BoxCollider2D>();
        lazerCollider.size = new Vector2(0.4f, finalLength);
    }

}
