using System.Collections;
using UnityEngine;

public class BossThree : Boss {
    [Header("Boss 3 Properties")]
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileBarrageAngle = 25f;
    [SerializeField] float missileFireInterval = 2;
    [SerializeField] int missileBulletsToFire = 5;
    [SerializeField] Transform missileSpawn1;
    [SerializeField] Transform missileSpawn2;
    [SerializeField] GameObject missileParticles;
    [SerializeField] GameObject trackingPrefab;
    [SerializeField] float trackingFireInterval = 2;
    [SerializeField] int trackingBulletsToFire = 5;
    [SerializeField] Transform trackingSpawn1;
    [SerializeField] Transform trackingSpawn2;
    [SerializeField] GameObject trackingParticles;
    [SerializeField] GameObject uncatchableBullet;
    [SerializeField] float uncatchableBulletFireInterval = 2;
    [SerializeField] int uncatchableBulletsToFire = 5;
    [SerializeField] WaveConfigSO minionWave1;
    [SerializeField] WaveConfigSO minionWave2;
    [SerializeField] WaveConfigSO minionWave3;
    [SerializeField] Material bossMaterial;

    private EnemySpawner enemySpawner;
    private bool wave1Spawned = false;
    private bool wave2Spawned = false;
    private bool wave3Spawned = false;

    // Unity Functions
    private void Update() {
        if (!wave1Spawned && health.GetFraction() < 0.75) {
            wave1Spawned = true;
            enemySpawner.StartWaveConfig(minionWave1);
        }

        if (!wave2Spawned && health.GetFraction() < 0.5) {
            wave2Spawned = true;
            enemySpawner.StartWaveConfig(minionWave2);
        }

        if (!wave3Spawned && health.GetFraction() < 0.25) {
            wave3Spawned = true;
            enemySpawner.StartWaveConfig(minionWave3);
        }
    }

    // Coroutines
    protected override IEnumerator BossCycle() {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        while(true) {
            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetMissileColor());
            deathParticles = missileParticles;
            health.SetHitParticleEffects(missileParticles);

            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < missileBulletsToFire; i++) {
                Instantiate(missilePrefab, missileSpawn1.position, Quaternion.identity);
                Instantiate(missilePrefab, missileSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(missileFireInterval);
            }
            yield return new WaitForSeconds(missileFireInterval);
            Instantiate(missilePrefab, missileSpawn1.position, Quaternion.Euler(0,0,-missileBarrageAngle));
            Instantiate(missilePrefab, missileSpawn1.position, Quaternion.identity);
            Instantiate(missilePrefab, missileSpawn1.position, Quaternion.Euler(0,0,missileBarrageAngle));
            Instantiate(missilePrefab, missileSpawn2.position, Quaternion.Euler(0,0,-missileBarrageAngle));
            Instantiate(missilePrefab, missileSpawn2.position, Quaternion.identity);
            Instantiate(missilePrefab, missileSpawn2.position, Quaternion.Euler(0,0,missileBarrageAngle));

            yield return new WaitForSeconds(missileFireInterval);
            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetTrackingColor());
            deathParticles = trackingParticles;
            health.SetHitParticleEffects(missileParticles);
            yield return new WaitForSeconds(timeBetweenPhases);

            for (int i = 0; i < trackingBulletsToFire; i++) {
                Instantiate(trackingPrefab, trackingSpawn1.position, Quaternion.identity);
                Instantiate(trackingPrefab, trackingSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(trackingFireInterval);
            }
            yield return new WaitForSeconds(trackingFireInterval);
            for (int i = 0; i < 8; i++) {
                if (i%2 == 0) {
                    Instantiate(trackingPrefab, trackingSpawn1.position, Quaternion.identity);
                } else {
                    Instantiate(trackingPrefab, trackingSpawn2.position, Quaternion.identity);
                }
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(trackingFireInterval);
            bossMaterial.SetColor("_ReplacedColor", Color.white);
            yield return new WaitForSeconds(timeBetweenPhases);

            for (int i = 0; i < uncatchableBulletsToFire; i++) {
                Instantiate(uncatchableBullet, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(uncatchableBulletFireInterval);
            }

            yield return new WaitForSeconds(uncatchableBulletFireInterval);
            Instantiate(trackingPrefab, trackingSpawn1.position, Quaternion.identity);
            Instantiate(trackingPrefab, trackingSpawn2.position, Quaternion.identity);
            Instantiate(missilePrefab, missileSpawn1.position, Quaternion.identity);
            Instantiate(missilePrefab, missileSpawn2.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            Instantiate(trackingPrefab, trackingSpawn1.position, Quaternion.identity);
            Instantiate(trackingPrefab, trackingSpawn2.position, Quaternion.identity);
            Instantiate(missilePrefab, missileSpawn1.position, Quaternion.identity);
            Instantiate(missilePrefab, missileSpawn2.position, Quaternion.identity);
            yield return new WaitForSeconds(uncatchableBulletFireInterval);
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
        UnlockAchievement("BEAT_LEVEL_3");
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadLevelFour();
    }
}
