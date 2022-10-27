using UnityEngine;

public class BulletMagnet : MonoBehaviour {
    [SerializeField] Transform bulletParent;
    [SerializeField] GameObject playerBulletPrefab;

    private bool magnetActive = false;
    private GameObject[] bulletArray = new GameObject[5];

    public void SetMagnetActive(bool magnetActive) {
        this.magnetActive = magnetActive;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!magnetActive) {
            return;
        }

        if (other.gameObject.tag == "Enemy Projectile") {
            Debug.Log("Bullet should be grabbed by player");
            Destroy(other.gameObject);
            AddEnemyBulletToArray(Instantiate(playerBulletPrefab));
        }
    }

    public void LaunchBullet() {
        if (bulletArray[0] == null) {
            Debug.Log("No Bullet to fire");
            return;
        }

        GameObject bulletToFire = bulletArray[0];
        bulletToFire.transform.parent = null;
        bulletToFire.GetComponent<Collider2D>().enabled = true;
        bulletToFire.GetComponent<PlayerProjectile>().enabled = true;

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
