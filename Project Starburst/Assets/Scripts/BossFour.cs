using System.Collections;
using UnityEngine;

public class BossFour : Boss {
    [Header("Boss 4 Properties")]
    [SerializeField] GameObject proxyMinePrefab;
    [SerializeField] float proxyMineFireInterval = 2;
    [SerializeField] int proxyMinesToFire = 5;
    [SerializeField] Transform proxyMineSpawn1;
    [SerializeField] Transform proxyMineSpawn2;
    [SerializeField] GameObject proxyMineParticles;
    [SerializeField] GameObject trackingPrefab;
    [SerializeField] float trackingFireInterval = 2;
    [SerializeField] int trackingBulletsToFire = 5;
    [SerializeField] Transform trackingSpawn1;
    [SerializeField] Transform trackingSpawn2;
    [SerializeField] GameObject trackingParticles;
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] Transform lazerSpawn1;
    [SerializeField] Transform lazerSpawn2;
    [SerializeField] Transform lazerSpawn3;
    [SerializeField] Material bossMaterial;

    private int laserType = 1;

    // Coroutines
    protected override IEnumerator BossCycle() {
        while(true) {
            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetProxyMineColor());
            deathParticles = proxyMineParticles;
            health.SetHitParticleEffects(proxyMineParticles);
            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < proxyMinesToFire; i++) {
                Instantiate(proxyMinePrefab, proxyMineSpawn1.position, Quaternion.identity);
                Instantiate(proxyMinePrefab, proxyMineSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(proxyMineFireInterval);
            }

            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetTrackingColor());
            deathParticles = trackingParticles;
            health.SetHitParticleEffects(trackingParticles);
            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < trackingBulletsToFire; i++) {
                Instantiate(trackingPrefab, trackingSpawn1.position, Quaternion.identity);
                Instantiate(trackingPrefab, trackingSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(trackingFireInterval);
            }

            yield return new WaitForSeconds(timeBetweenPhases);
            SpawnLasers(laserType);

            if (laserType == 2) {
                yield return new WaitForSeconds(timeBetweenPhases);
                for (int i = 0; i < 7; i++) {
                    Instantiate(trackingPrefab, trackingSpawn1.position, Quaternion.identity);
                    yield return new WaitForSeconds(trackingFireInterval / 4);
                    Instantiate(trackingPrefab, trackingSpawn2.position, Quaternion.identity);
                    yield return new WaitForSeconds(trackingFireInterval / 4);
                }
            } else if (laserType == 1) {
                bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetProxyMineColor());
                deathParticles = proxyMineParticles;
                health.SetHitParticleEffects(proxyMineParticles);
                yield return new WaitForSeconds(timeBetweenPhases);

                for (int i = 0; i < 8; i++) {
                    Instantiate(proxyMinePrefab,
                                transform.position + new Vector3(Random.Range(-1.5f, 1.5f), 0, 0),
                                Quaternion.identity);
                    yield return new WaitForSeconds(proxyMineFireInterval / 2);
                }
            }

            yield return new WaitForSeconds(timeBetweenPhases);
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
        UnlockAchievement("BEAT_LEVEL_4");
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadLevelFive();
    }

    private void SpawnLasers(int spawnType) {
        if (spawnType == 1) {
            Instantiate(lazerPrefab, lazerSpawn1.position, Quaternion.identity);
            laserType = 2;
        } else if (spawnType == 2) {
            Instantiate(lazerPrefab, lazerSpawn2.position, Quaternion.identity);
            Instantiate(lazerPrefab, lazerSpawn3.position, Quaternion.identity);
            laserType = 1;
        }
    }
}
