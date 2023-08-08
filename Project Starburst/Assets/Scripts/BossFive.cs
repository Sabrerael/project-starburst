using System.Collections;
using UnityEngine;

public class BossFive : Boss {
    [Header("Boss 5 Properties")]
    [SerializeField] GameObject boomerangPrefab;
    [SerializeField] float boomerangFireInterval = 2;
    [SerializeField] int boomerangsToFire = 5;
    [SerializeField] Transform boomerangSpawn1;
    [SerializeField] Transform boomerangSpawn2;
    [SerializeField] GameObject boomerangParticles;
    [SerializeField] GameObject proxyMinePrefab;
    [SerializeField] float proxyMineFireInterval = 2;
    [SerializeField] int proxyMinesToFire = 5;
    [SerializeField] Transform proxyMineSpawn1;
    [SerializeField] Transform proxyMineSpawn2;
    [SerializeField] GameObject proxyMineParticles;
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
        UnlockAchievement("BEAT_LEVEL_5");
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadLevelSix();
    }
}
