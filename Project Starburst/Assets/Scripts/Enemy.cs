using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] GameObject projectile = null;

    private void Start() {
        StartCoroutine(FireProjectiles(2));
    }

    private IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
