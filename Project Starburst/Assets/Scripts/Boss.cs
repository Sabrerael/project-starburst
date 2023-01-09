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
    [SerializeField] Material basicEnemyMaterialColorBlind;
    [SerializeField] float basicBulletFireInterval = 2;
    [SerializeField] int basicBulletsToFire = 5;
    [SerializeField] GameObject basicEnemyParticles;

    [Header("Missile Properties")]
    [SerializeField] GameObject missile;
    [SerializeField] Material missileEnemyMaterial;
    [SerializeField] Material missileEnemyMaterialColorBlind;
    [SerializeField] float missileFireInterval = 2;
    [SerializeField] int missilesToFire = 5;
    [SerializeField] Transform missileSpawn1;
    [SerializeField] Transform missileSpawn2;
    [SerializeField] GameObject missileEnemyParticles;
    
    [Header("Piercing Bullet Properties")]
    [SerializeField] GameObject piercingBullet;
    [SerializeField] Material piercingEnemyMaterial;
    [SerializeField] Material piercingEnemyMaterialColorBlind;
    [SerializeField] float piercingBulletFireInterval = 2;
    [SerializeField] int piercingBulletsToFire = 5;
    [SerializeField] Transform piercingSpawn1;
    [SerializeField] Transform piercingSpawn2;
    [SerializeField] GameObject piercingEnemyParticles;

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
                Instantiate(missile, missileSpawn1.position, Quaternion.identity);
                Instantiate(missile, missileSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(missileFireInterval);
            }
            
            spriteRenderer.material = piercingEnemyMaterial;
            yield return new WaitForSeconds(timeBetweenPhases);

            for (int i = 0; i < piercingBulletsToFire; i++) {
                Instantiate(piercingBullet, piercingSpawn1.position, Quaternion.identity);
                Instantiate(piercingBullet, piercingSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(piercingBulletFireInterval);
            }
        }
        
    }
}
