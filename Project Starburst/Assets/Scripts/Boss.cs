using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour {
    [Header("General Properties")]
    [SerializeField] int scoreValue = 10000;
    [SerializeField] float timeBetweenPhases = 1.5f;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Basic Bullet Properties")]
    [SerializeField] GameObject basicBullet;
    [SerializeField] Material basicEnemyMaterial;
    [SerializeField] float basicBulletFireInterval = 2;
    [SerializeField] int basicBulletsToFire = 5;

    [Header("Missile Properties")]
    [SerializeField] GameObject missile;
    [SerializeField] Material missileEnemyMaterial;
    [SerializeField] float missileFireInterval = 2;
    [SerializeField] int missilesToFire = 5;
    
    [Header("Piercing Bullet Properties")]
    [SerializeField] GameObject piercingBullet;
    [SerializeField] Material piercingEnemyMaterial;
    [SerializeField] float piercingBulletFireInterval = 2;
    [SerializeField] int piercingBulletsToFire = 5;

    private Health health;

    private void Start() {
        StartCoroutine(BossCycle());
    }

    public void AddScoreToPlayer() {
        FindObjectOfType<Player>().AddToTotalScore(scoreValue);
    }

    private IEnumerator BossCycle() {
        while(true) {
            spriteRenderer.material = basicEnemyMaterial;
            yield return new WaitForSeconds(timeBetweenPhases);

            for (int i = 0; i < basicBulletsToFire; i++) {
                Instantiate(basicBullet, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(basicBulletFireInterval);
            }

            spriteRenderer.material = missileEnemyMaterial;
            yield return new WaitForSeconds(timeBetweenPhases);
            
            for (int i = 0; i < missilesToFire; i++) {
                Instantiate(missile, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(missileFireInterval);
            }
            
            spriteRenderer.material = piercingEnemyMaterial;
            yield return new WaitForSeconds(timeBetweenPhases);

            for (int i = 0; i < piercingBulletsToFire; i++) {
                Instantiate(piercingBullet, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(piercingBulletFireInterval);
            }
        }
        
    }
}
