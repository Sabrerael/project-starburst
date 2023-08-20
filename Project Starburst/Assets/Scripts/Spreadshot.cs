using UnityEngine;

public class Spreadshot : Projectile {
    [SerializeField] Projectile smallerSpreadshot;

    protected override void Start() {
        base.Start();
        if (isPlayerProjectile) {
            Instantiate(smallerSpreadshot, transform.position + new Vector3(0.2f,0,0), Quaternion.Euler(0,0,-25));
            Instantiate(smallerSpreadshot, transform.position, Quaternion.identity);
            Instantiate(smallerSpreadshot, transform.position + new Vector3(-0.2f,0,0), Quaternion.Euler(0,0,25));
            Destroy(gameObject);
        }   
    }
}
