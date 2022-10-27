using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] BulletMagnet bulletMagnet;

    // CACHE
    private Movement movement;

    private void Start() {
        movement = GetComponent<Movement>();
    }

    private void OnFire(InputValue value) {
        if (value.isPressed) {
            bulletMagnet.LaunchBullet();
        }
    }

    private void OnMagnet(InputValue value) {
        if (value.isPressed) {
            bulletMagnet.SetMagnetActive(true);
        } else if (!value.isPressed) {
            bulletMagnet.SetMagnetActive(false);
        }
    }

    private void OnMove(InputValue value) {
        movement.SetMovementValues(value.Get<Vector2>());
    }
    //This really shouldn't go in here but I'll move it later
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy Projectile") {
            Debug.Log("Enemy Projectile hit " + name);
            int damage = other.gameObject.GetComponent<EnemyProjectile>().GetDamage();
            GetComponent<Health>().ModifyHealthPoints(-damage);
            Destroy(other.gameObject);
        }
    }

}
