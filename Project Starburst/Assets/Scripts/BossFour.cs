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
    [SerializeField] GameObject uncatchableBullet;
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] float uncatchableBulletFireInterval = 2;
    [SerializeField] int uncatchableBulletsToFire = 5;
    [SerializeField] Material bossMaterial;

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

            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < trackingBulletsToFire; i++) {
                Instantiate(trackingPrefab, trackingSpawn1.position, Quaternion.identity);
                Instantiate(trackingPrefab, trackingSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(trackingFireInterval);
            }

            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < uncatchableBulletsToFire; i++) {
                Instantiate(uncatchableBullet, transform.position, Quaternion.identity);
                Instantiate(uncatchableBullet, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(uncatchableBulletFireInterval);
            }
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
}
