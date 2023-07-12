using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFive : Boss {
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
