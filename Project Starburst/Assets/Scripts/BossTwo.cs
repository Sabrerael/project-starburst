using System.Collections;
using UnityEngine;

public class BossTwo : Boss {
    [Header("Boss 2 Properties")]
    [SerializeField] GameObject spreadshotBullet;
    [SerializeField] float spreadshotBulletFireInterval = 2;
    [SerializeField] int spreadshotBulletsToFire = 5;
    [SerializeField] Transform spreadshotSpawn1;
    [SerializeField] Transform spreadshotSpawn2;
    [SerializeField] GameObject uncatchableBullet;
    [SerializeField] float uncatchableBulletFireInterval = 2;
    [SerializeField] int uncatchableBulletsToFire = 5;
    [SerializeField] GameObject basicShield;
    [SerializeField] GameObject basicShieldBroken;
    [SerializeField] WaveConfigSO basicEnemyWave;
    [SerializeField] GameObject piercingShield;
    [SerializeField] GameObject piercingShieldBroken;
    [SerializeField] WaveConfigSO piercingEnemyWave;
    [SerializeField] GameObject hitParticles;
    [SerializeField] Material bossMaterial;

    private bool basicShieldUp = false;
    private bool piercingShieldUp = false;
    private EnemySpawner enemySpawner;

    // Unity Functions
    private void Update() {
        if (!basicShieldUp && health.GetFraction() < 0.66) {
            health.SetShieldObject(Instantiate(basicShield, transform));
            var brokenShieldObject = Instantiate(basicShieldBroken, transform);
            health.SetBrokenShieldObject(brokenShieldObject);
            brokenShieldObject.SetActive(false);
            health.SetShieldWeakness(BulletType.Basic);
            basicShieldUp = true;
            enemySpawner.StartWaveConfig(basicEnemyWave);
        }

        if (!piercingShieldUp && health.GetFraction() < 0.33) {
            health.SetShieldObject(Instantiate(piercingShield, transform));
            var brokenShieldObject = Instantiate(piercingShieldBroken, transform);
            health.SetBrokenShieldObject(brokenShieldObject);
            brokenShieldObject.SetActive(false);
            health.SetShieldWeakness(BulletType.Piercing);
            piercingShieldUp = true;
            enemySpawner.StartWaveConfig(piercingEnemyWave);
        }
    }

    // Coroutines

    protected override IEnumerator BossCycle() {
        // TODO This needs to be updated to not just be Yellow
        bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetLevel2BossColor());
        enemySpawner = FindObjectOfType<EnemySpawner>();
        deathParticles = hitParticles;

        while(true) {
            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < spreadshotBulletsToFire; i++) {
                Instantiate(spreadshotBullet, spreadshotSpawn1.position, Quaternion.identity);
                Instantiate(spreadshotBullet, spreadshotSpawn2.position, Quaternion.identity);
                yield return new WaitForSeconds(spreadshotBulletFireInterval);
            }

            bossMaterial.SetColor("_ReplacedColor", Color.white);
            yield return new WaitForSeconds(timeBetweenPhases);
            for (int i = 0; i < uncatchableBulletsToFire; i++) {
                Instantiate(uncatchableBullet, transform.position + new Vector3(0,-0.5f, 0), Quaternion.identity);
                yield return new WaitForSeconds(uncatchableBulletFireInterval);
            }
            bossMaterial.SetColor("_ReplacedColor", SettingsManager.GetLevel2BossColor());
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
        //UnlockAchievement("BEAT_LEVEL_2");
        Destroy(gameObject);
        FindObjectOfType<LevelLoader>().LoadWinScreen();
    }
}
