using UnityEngine;

public class BulletMagnet : MonoBehaviour {
    [SerializeField] Transform bulletParent;
    [SerializeField] GameObject playerBulletPrefab;

    [SerializeField] float magnetDrain = 2;
    [SerializeField] float magnetRegen = 4;

    private bool magnetActive = false;
    private float magnetPower = 1;
    private GameObject[] bulletArray = new GameObject[5];
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (!magnetActive && magnetPower != 1) {
            magnetPower = Mathf.Clamp(magnetPower + (magnetRegen * Time.deltaTime), 0, 1);
            return;
        } 

        magnetPower = Mathf.Clamp(magnetPower - (magnetDrain * Time.deltaTime), 0, 1);

        if (magnetPower == 0) {
            magnetActive = false;
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!magnetActive) {
            return;
        }

        if (other.gameObject.tag == "Enemy Projectile") {
            Destroy(other.gameObject);
            AddEnemyBulletToArray(Instantiate(playerBulletPrefab));
        }
    }

    public float GetMagnetPower() { return magnetPower; }

    public void SetMagnetActive(bool magnetActive) {
        this.magnetActive = magnetActive;
        spriteRenderer.enabled = magnetActive;
    }

    public void LaunchBullet() {
        if (bulletArray[0] == null) {
            Debug.Log("No Bullet to fire");
            return;
        }

        GameObject bulletToFire = bulletArray[0];
        bulletToFire.transform.parent = null;
        bulletToFire.GetComponent<Collider2D>().enabled = true;
        bulletToFire.GetComponent<Projectile>().enabled = true;

        ResetBulletArray();
    }

    private void AddEnemyBulletToArray(GameObject gameObject) {
        for (int i = 0; i < bulletArray.Length; i++) {
            if (bulletArray[i] == null) {
                bulletArray[i] = gameObject;
                gameObject.transform.parent = bulletParent;
                gameObject.transform.position = transform.position + new Vector3(0, 1f, 0);
                return;
            }
        }
    }

    private void ResetBulletArray() {
        for (int i = 1; i < bulletArray.Length; i++) {
            bulletArray[i-1] = bulletArray[i];
        }
    }
}
