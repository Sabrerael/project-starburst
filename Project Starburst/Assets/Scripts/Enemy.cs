using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] GameObject projectile;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] int scoreValue = 100;
    [SerializeField] float fireInterval = 2;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material colorblindMaterial;

    private Health health;

    private void Start() {
        health = GetComponent<Health>();
        SetMaterial();
        SettingsManager.onSettingsChange += SetMaterial;
        StartCoroutine(FireProjectiles(fireInterval));
    }

    public void AddScoreToPlayer() {
        FindObjectOfType<Player>().AddToTotalScore(scoreValue);
    }

    private void SetMaterial() {
        if (SettingsManager.GetColorSet() != 0) {
            spriteRenderer.material = colorblindMaterial;
            trailRenderer.material = colorblindMaterial;
        } else {
            spriteRenderer.material = defaultMaterial;
            trailRenderer.material = defaultMaterial;
        }
    }

    private IEnumerator FireProjectiles(float time) {
        while (true) {
            Instantiate(projectile, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
