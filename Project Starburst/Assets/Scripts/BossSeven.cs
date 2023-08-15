using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSeven : Boss {
    [Header("Boss 7 Properties")]
    [SerializeField] Material bossMaterial;
    [SerializeField] WaveConfigSO[] spawnableWaves;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject basicPrefab;
    [SerializeField] float basicFireInterval = 2;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileFireInterval = 2;
    [SerializeField] GameObject piercingPrefab;
    [SerializeField] float piercingFireInterval = 2;
    [SerializeField] GameObject spreadshotPrefab;
    [SerializeField] float spreadshotFireInterval = 2;
    [SerializeField] GameObject boomerangPrefab;
    [SerializeField] float boomerangFireInterval = 2;
    [SerializeField] GameObject proxyMinePrefab;
    [SerializeField] float proxyMineFireInterval = 2;
    [SerializeField] GameObject trackingPrefab;
    [SerializeField] float trackingFireInterval = 2;

    [SerializeField] GameObject trackingParticles;

    private EnemySpawner enemySpawner;
    private BossState state = BossState.Starting;
    private bool spawning = false;

    // Unity Functions
    private void Update() {
        if (enemySpawner.transform.childCount > 1) {
            return;
        }

        Debug.Log(state);
        Debug.Log(health.GetFraction());

        if (state == BossState.BasicSpawning && !spawning && enemySpawner.transform.childCount == 1) {
            shield.SetActive(false);
            state = BossState.BasicFiring;
        } else if (state == BossState.BasicFiring && health.GetFraction() <= (6f/7f)) {
            shield.SetActive(true);
            state = BossState.MissileSpawning;
            spawning = true;
        } else if (state == BossState.MissileSpawning && !spawning && enemySpawner.transform.childCount == 1) {
            shield.SetActive(false);
            state = BossState.MissileFiring;
        } else if (state == BossState.MissileFiring && health.GetFraction() <= (5f/7f)) {
            Debug.Log("In if statement");
            shield.SetActive(true);
            state = BossState.PiercingSpawning;
            spawning = true;
        } else if (state == BossState.PiercingSpawning && !spawning && enemySpawner.transform.childCount == 1) {
            shield.SetActive(false);
            state = BossState.PiercingFiring;
        } else if (state == BossState.PiercingFiring && health.GetFraction() <= (4f/7f)) {
            shield.SetActive(true);
            state = BossState.SpreadshotSpawning;
            spawning = true;
        } else if (state == BossState.SpreadshotSpawning && !spawning && enemySpawner.transform.childCount == 1) {
            shield.SetActive(false);
            state = BossState.SpreadshotFiring;
        } else if (state == BossState.SpreadshotFiring && health.GetFraction() <= (3f/7f)) {
            shield.SetActive(true);
            state = BossState.BoomerangSpawning;
            spawning = true;
        } else if (state == BossState.BoomerangSpawning && !spawning && enemySpawner.transform.childCount == 1) {
            shield.SetActive(false);
            state = BossState.BoomerangFiring;
        } else if (state == BossState.BoomerangFiring && health.GetFraction() <= (2f/7f)) {
            shield.SetActive(true);
            state = BossState.ProxyMineSpawning;
            spawning = true;
        } else if (state == BossState.ProxyMineSpawning && !spawning && enemySpawner.transform.childCount == 1) {
            shield.SetActive(false);
            state = BossState.ProxyMineFiring;
        } else if (state == BossState.ProxyMineFiring && health.GetFraction() <= (1f/7f)) {
            shield.SetActive(true);
            state = BossState.TrackingSpawning;
            spawning = true;
        } else if (state == BossState.TrackingSpawning && !spawning && enemySpawner.transform.childCount == 1) {
            shield.SetActive(false);
            state = BossState.TrackingFiring;
        }
    }

    // Coroutines

    protected override IEnumerator BossCycle() {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        deathParticles = trackingParticles;
        bossMaterial.SetColor("_ReplacedColor", Color.white);
        yield return new WaitForSeconds(timeBetweenPhases*2);

        while(true) {
            if (state == BossState.Starting) {
                enemySpawner.StartWaveConfig(spawnableWaves[0]);
                yield return new WaitForSeconds(timeBetweenPhases);
                state = BossState.BasicSpawning;
                spawning = true;
                bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetBasicColor());
                yield return new WaitForSeconds(5);
                spawning = false;
            } else if (state == BossState.BasicFiring) {            
                Instantiate(basicPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(basicFireInterval);
            } else if (state == BossState.MissileSpawning && spawning) {
                enemySpawner.StartWaveConfig(spawnableWaves[1]);
                yield return new WaitForSeconds(timeBetweenPhases);
                bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetMissileColor());
                yield return new WaitForSeconds(5);
                spawning = false;
            } else if (state == BossState.MissileFiring) {            
                Instantiate(missilePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(missileFireInterval);
            } else if (state == BossState.PiercingSpawning && spawning) {
                enemySpawner.StartWaveConfig(spawnableWaves[2]);
                yield return new WaitForSeconds(timeBetweenPhases);
                bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetPiercingColor());
                yield return new WaitForSeconds(5);
                spawning = false;
            } else if (state == BossState.PiercingFiring) {            
                Instantiate(piercingPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(piercingFireInterval);
            } else if (state == BossState.SpreadshotSpawning && spawning) {
                enemySpawner.StartWaveConfig(spawnableWaves[3]);
                yield return new WaitForSeconds(timeBetweenPhases);
                 bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetLevel2BossColor());
                yield return new WaitForSeconds(5);
                spawning = false;
            } else if (state == BossState.SpreadshotFiring) {            
                Instantiate(spreadshotPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spreadshotFireInterval);
            } else if (state == BossState.BoomerangSpawning && spawning) {
                enemySpawner.StartWaveConfig(spawnableWaves[4]);
                yield return new WaitForSeconds(timeBetweenPhases);
                bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetBoomerangColor());
                yield return new WaitForSeconds(5);
                spawning = false;
            } else if (state == BossState.BoomerangFiring) {            
                Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(boomerangFireInterval);
            } else if (state == BossState.ProxyMineSpawning && spawning) {
                enemySpawner.StartWaveConfig(spawnableWaves[5]);
                yield return new WaitForSeconds(timeBetweenPhases);
                 bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetProxyMineColor());
                yield return new WaitForSeconds(5);
                spawning = false;
            } else if (state == BossState.ProxyMineFiring) {            
                Instantiate(proxyMinePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(proxyMineFireInterval);
            } else if (state == BossState.TrackingSpawning && spawning) {
                enemySpawner.StartWaveConfig(spawnableWaves[6]);
                yield return new WaitForSeconds(timeBetweenPhases);
                 bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetTrackingColor());
                yield return new WaitForSeconds(5);
                spawning = false;
            } else if (state == BossState.TrackingFiring) {            
                Instantiate(trackingPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(trackingFireInterval);
            }
            else {
                yield return new WaitForSeconds(timeBetweenPhases);
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
        UnlockAchievement("BEAT_LEVEL_7");
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadWinScreen();
    }

    private enum BossState {
        Starting,
        BasicSpawning,
        BasicFiring,
        MissileSpawning,
        MissileFiring,
        PiercingSpawning,
        PiercingFiring,
        SpreadshotSpawning,
        SpreadshotFiring,
        BoomerangSpawning,
        BoomerangFiring,
        ProxyMineSpawning,
        ProxyMineFiring,
        TrackingSpawning,
        TrackingFiring
    }
}
