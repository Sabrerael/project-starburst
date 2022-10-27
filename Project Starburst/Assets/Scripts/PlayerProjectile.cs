using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField] float movementSpeed = 2.5f;
    [SerializeField] int damage = 50;

    private void Update() {
        transform.position += new Vector3(0, movementSpeed * Time.deltaTime, 0);
    }

    public int GetDamage() { return damage; }
}
