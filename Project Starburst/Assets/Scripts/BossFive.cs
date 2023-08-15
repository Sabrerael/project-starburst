using System.Collections;
using UnityEngine;

public class BossFive : Boss {
    [Header("Boss 5 Properties")]
    [SerializeField] GameObject boomerangPrefab;
    [SerializeField] float boomerangFireInterval = 2;
    [SerializeField] int boomerangsToFire = 5;
    [SerializeField] GameObject boomerangParticles;
    [SerializeField] GameObject proxyMinePrefab;
    [SerializeField] float proxyMineFireInterval = 2;
    [SerializeField] int proxyMinesToFire = 5;
    [SerializeField] GameObject proxyMineParticles;
    [SerializeField] Transform[] projectileSpawns;
    [SerializeField] Material bossMaterial;

    private int indexToShoot = 0;

    private void Update() {
        if (transform.position.y <= 0) {
            indexToShoot = 2;
        } else {
            indexToShoot = 0;
        }
    }

    // Coroutines
    protected override IEnumerator BossCycle() {
        yield return new WaitForSeconds(timeBetweenPhases);
        while(true) {
            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetBoomerangColor());
            deathParticles = boomerangParticles;
            health.SetHitParticleEffects(boomerangParticles);
            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < boomerangsToFire; i++) {
                Instantiate(boomerangPrefab, projectileSpawns[indexToShoot].position, projectileSpawns[indexToShoot].rotation);
                Instantiate(boomerangPrefab, projectileSpawns[indexToShoot+1].position, projectileSpawns[indexToShoot+1].rotation);
                yield return new WaitForSeconds(boomerangFireInterval);
            }

            yield return new WaitForSeconds(timeBetweenPhases);

            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetProxyMineColor());
            deathParticles = proxyMineParticles;
            health.SetHitParticleEffects(proxyMineParticles);
            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < proxyMinesToFire; i++) {
                Instantiate(proxyMinePrefab, projectileSpawns[indexToShoot].position, projectileSpawns[indexToShoot].rotation);
                Instantiate(proxyMinePrefab, projectileSpawns[indexToShoot+1].position, projectileSpawns[indexToShoot+1].rotation);
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
