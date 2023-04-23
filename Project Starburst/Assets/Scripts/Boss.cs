using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour {
    [Header("General Properties")]
    [SerializeField] int scoreValue = 10000;
    [SerializeField] float timeBetweenPhases = 1.5f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AudioClip explosionSFX;

    [Header("Basic Bullet Properties")]
    [SerializeField] GameObject basicBullet;
    [SerializeField] Material basicEnemyMaterial;
    [SerializeField] Material basicEnemyMaterialColorBlind;
    [SerializeField] float basicBulletFireInterval = 2;
    [SerializeField] int basicBulletsToFire = 5;
    [SerializeField] GameObject basicEnemyParticles;
    [SerializeField] GameObject basicEnemyParticlesColorBlind;

    [Header("Missile Properties")]
    [SerializeField] GameObject missile;
    [SerializeField] Material missileEnemyMaterial;
    [SerializeField] Material missileEnemyMaterialColorBlind;
    [SerializeField] float missileFireInterval = 2;
    [SerializeField] int missilesToFire = 5;
    [SerializeField] Transform missileSpawn1;
    [SerializeField] Transform missileSpawn2;
    [SerializeField] GameObject missileEnemyParticles;
    [SerializeField] GameObject missileEnemyParticlesColorBlind;
    
    [Header("Piercing Bullet Properties")]
    [SerializeField] GameObject piercingBullet;
    [SerializeField] Material piercingEnemyMaterial;
    [SerializeField] Material piercingEnemyMaterialColorBlind;
    [SerializeField] float piercingBulletFireInterval = 2;
    [SerializeField] int piercingBulletsToFire = 5;
    [SerializeField] Transform piercingSpawn1;
    [SerializeField] Transform piercingSpawn2;
    [SerializeField] GameObject piercingEnemyParticles;
    [SerializeField] GameObject piercingEnemyParticlesColorBlind;

    private Health health;
    private bool useColorBlindScheme = false;
    private GameObject deathParticles;

    private void Start() {
        health = GetComponent<Health>();
        SetMaterials();
        SettingsManager.onSettingsChange += SetMaterials;
        StartCoroutine(BossCycle());
    }

    public void AddScoreToPlayer() {
        FindObjectOfType<Player>().AddToTotalScore(scoreValue);
    }

    public void DeathAnimation() {
        StopAllCoroutines();
        StartCoroutine(BossDeath());
    }

    private void SetMaterials() {
        /*if (SettingsManager.GetColorSet() == 0) {
            useColorBlindScheme = false;
        } else {
            useColorBlindScheme = true;
        }*/
    }

    private IEnumerator BossCycle() {
        while(true) {
            health.SetHitParticleEffects(basicEnemyParticles, basicEnemyParticlesColorBlind);
            if (useColorBlindScheme) {
                spriteRenderer.material = basicEnemyMaterialColorBlind;
                deathParticles = basicEnemyParticlesColorBlind;
            } else {
                spriteRenderer.material = basicEnemyMaterial;
                deathParticles = basicEnemyParticles;
            }
            yield return new WaitForSeconds(timeBetweenPhases);

            for (int i = 0; i < basicBulletsToFire; i++) {
                Instantiate(basicBullet, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.35f);
                Instantiate(basicBullet, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.35f);
                Instantiate(basicBullet, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(basicBulletFireInterval);
            }

            health.SetHitParticleEffects(missileEnemyParticles, missileEnemyParticlesColorBlind);
            if (useColorBlindScheme) {
                spriteRenderer.material = missileEnemyMaterialColorBlind;
                deathParticles = missileEnemyParticlesColorBlind;
            } else {
                spriteRenderer.material = missileEnemyMaterial;
                deathParticles = missileEnemyParticles;
            }
            yield return new WaitForSeconds(timeBetweenPhases);
            
            for (int i = 0; i < missilesToFire; i++) {
                Instantiate(missile, missileSpawn1.position, Quaternion.identity);
                Instantiate(missile, missileSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(missileFireInterval);
            }

            health.SetHitParticleEffects(piercingEnemyParticles, piercingEnemyParticlesColorBlind);
            if (useColorBlindScheme) {
                spriteRenderer.material = piercingEnemyMaterialColorBlind;
                deathParticles = piercingEnemyParticlesColorBlind;
            } else {
                spriteRenderer.material = piercingEnemyMaterial;
                deathParticles = piercingEnemyParticles;
            }
            yield return new WaitForSeconds(timeBetweenPhases);

            for (int i = 0; i < piercingBulletsToFire; i++) {
                Instantiate(piercingBullet, piercingSpawn1.position, Quaternion.identity);
                Instantiate(piercingBullet, piercingSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(0.35f);
                Instantiate(piercingBullet, piercingSpawn1.position, Quaternion.identity);
                Instantiate(piercingBullet, piercingSpawn2.position, Quaternion.identity);                                
                yield return new WaitForSeconds(piercingBulletFireInterval);
            }
        }
    }

    private IEnumerator BossDeath() {
        GameObject bossCrossFade = GameObject.Find("Boss Crossfade");
        GetComponent<Pathfinder>().enabled = false;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = explosionSFX;
        for (int i = 0; i < 5; i++) {
            Vector3 particleInstantationPoint = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f)) + 
                                                transform.position;
            Instantiate(deathParticles, particleInstantationPoint, Quaternion.identity, transform);
            audioSource.Play();
            yield return new WaitForSeconds(0.5f);
        }
        bossCrossFade.GetComponent<Animator>().SetTrigger("Start");
        for (int i = 0; i < 16; i++) {
            Vector3 particleInstantationPoint = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f)) + 
                                                transform.position;
            Instantiate(deathParticles, particleInstantationPoint, Quaternion.identity, transform);
            audioSource.Play();
            yield return new WaitForSeconds(0.125f);
        }
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadLevelTwo();
    }
}
