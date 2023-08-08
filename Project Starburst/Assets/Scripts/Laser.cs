using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField] float finalLength = 20f;
    [SerializeField] float extensionTime = 0.5f;
    [SerializeField] int damagePerTick = 20;
    [SerializeField] float timeBetweenTicks = 1f;
    [SerializeField] float lifetime = 15f;

    float extensionTimer = 0;
    float damageTimer = 0;
    int tickCounter = 0;
    SpriteRenderer spriteRenderer;
    BoxCollider2D lazerCollider;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(0.5f, finalLength);
        lazerCollider = GetComponent<BoxCollider2D>();
        lazerCollider.size = new Vector2(0.4f, finalLength);
        Destroy(this.gameObject, lifetime); // Works but looks a little funky. TODO Improve this 
    }

    private void Update() {
        if (extensionTimer <= extensionTime) {
            extensionTimer += Time.deltaTime;
            spriteRenderer.size = new Vector2(0.5f, Mathf.Lerp(spriteRenderer.size.y, finalLength, extensionTimer / extensionTime));
            lazerCollider.size = new Vector2(0.4f, Mathf.Lerp(spriteRenderer.size.y, finalLength, extensionTimer / extensionTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damagePerTick, BulletType.None);
            damageTimer += Time.deltaTime;
            tickCounter++;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag != "Player") { return; }

        damageTimer += Time.deltaTime;
        if (damageTimer / timeBetweenTicks >= tickCounter) {
            other.gameObject.GetComponent<Health>().ModifyHealthPoints(-damagePerTick, BulletType.None);
            tickCounter++;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        damageTimer = 0;
        tickCounter = 0;
    }
}
