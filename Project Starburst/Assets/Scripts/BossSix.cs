using System.Collections;
using UnityEngine;

public class BossSix : Boss {
    [Header("Boss 6 Properties")]
    [SerializeField] Transform projectileSpawn;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileFireInterval = 2;
    [SerializeField] int missileBulletsToFire = 5;
    [SerializeField] GameObject missileParticles;
    [SerializeField] GameObject piercingBullet;
    [SerializeField] float piercingBulletFireInterval = 2;
    [SerializeField] int piercingBulletsToFire = 5;
    [SerializeField] GameObject piercingEnemyParticles;
    [SerializeField] GameObject spreadshotBullet;
    [SerializeField] float spreadshotBulletFireInterval = 2;
    [SerializeField] int spreadshotBulletsToFire = 5;
    [SerializeField] GameObject spreadshotEnemyParticles;
    [SerializeField] GameObject trackingPrefab;
    [SerializeField] float trackingFireInterval = 2;
    [SerializeField] int trackingBulletsToFire = 5;
    [SerializeField] GameObject trackingParticles;
    [SerializeField] Material bossMaterial;
    [SerializeField] Enemy[] missileEnemies;
    [SerializeField] Enemy[] piercingEnemies;
    [SerializeField] Enemy[] spreadshotEnemies;
    [SerializeField] Enemy[] trackingEnemies;

    // Functions

    private void ToggleEnemiesFiring (Enemy[] enemies) {
        foreach (Enemy enemy in enemies) {
            enemy.ToggleFiring();
        }
    }

    // Coroutines

    protected override IEnumerator BossCycle() {
        bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetMissileColor());
        deathParticles = missileParticles;
        health.SetHitParticleEffects(missileParticles);
        yield return new WaitForSeconds(timeBetweenPhases);
        Debug.Log("Starting cycle");
        while(true) {
            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetMissileColor());
            deathParticles = missileParticles;
            health.SetHitParticleEffects(missileParticles);
            ToggleEnemiesFiring(missileEnemies);

            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < missileBulletsToFire; i++) {
                Instantiate(missilePrefab, projectileSpawn.position, Quaternion.identity);
                yield return new WaitForSeconds(missileFireInterval);
            }
            yield return new WaitForSeconds(missileFireInterval);
            ToggleEnemiesFiring(missileEnemies);

            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetPiercingColor());
            deathParticles = piercingEnemyParticles;
            health.SetHitParticleEffects(piercingEnemyParticles);
            ToggleEnemiesFiring(piercingEnemies);

            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < piercingBulletsToFire; i++) {
                Instantiate(piercingBullet, projectileSpawn.position, Quaternion.identity);
                yield return new WaitForSeconds(piercingBulletFireInterval);
            }
            
            yield return new WaitForSeconds(piercingBulletFireInterval);
            ToggleEnemiesFiring(piercingEnemies);

            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetLevel2BossColor());
            deathParticles = spreadshotEnemyParticles;
            health.SetHitParticleEffects(spreadshotEnemyParticles);
            ToggleEnemiesFiring(spreadshotEnemies);

            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < spreadshotBulletsToFire; i++) {
                Instantiate(spreadshotBullet, projectileSpawn.position, Quaternion.identity);
                yield return new WaitForSeconds(spreadshotBulletFireInterval);
            }

            yield return new WaitForSeconds(spreadshotBulletFireInterval);
            ToggleEnemiesFiring(spreadshotEnemies);

            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetTrackingColor());
            deathParticles = trackingParticles;
            health.SetHitParticleEffects(trackingParticles);
            ToggleEnemiesFiring(trackingEnemies);

            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < trackingBulletsToFire; i++) {
                Instantiate(trackingPrefab, projectileSpawn.position, Quaternion.identity);
                yield return new WaitForSeconds(trackingFireInterval);
            }
            yield return new WaitForSeconds(trackingFireInterval);
            ToggleEnemiesFiring(trackingEnemies);
        }
    }

    protected override IEnumerator BossDeath() {
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
        UnlockAchievement("BEAT_LEVEL_6");
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadLevelSeven();
    }
}
