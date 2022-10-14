using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    [SerializeField] float movementSpeed = 2.5f;

    private Rigidbody2D projectileRB;

    private void Awake() {
        projectileRB = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        projectileRB.velocity = new Vector3(0, -movementSpeed, 0);
    }
}
