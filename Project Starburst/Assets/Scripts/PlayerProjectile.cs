using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField] float movementSpeed = 2.5f;

    private void Update() {
        transform.position += new Vector3(0, movementSpeed * Time.deltaTime, 0);
    }
}
