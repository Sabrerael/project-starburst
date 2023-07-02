using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    [Header("General Properties")]
    [SerializeField] protected int scoreValue = 10000;
    [SerializeField] protected float timeBetweenPhases = 1.5f;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected AudioClip explosionSFX;

    protected Health health;
    protected GameObject deathParticles;

    private void Start() {
        health = GetComponent<Health>();
        StartCoroutine(BossCycle());
    }

    public void AddScoreToPlayer() {
        FindObjectOfType<Player>().AddToTotalScore(scoreValue);
    }

    public void DeathAnimation() {
        StopAllCoroutines();
        StartCoroutine(BossDeath());
    }

    protected virtual IEnumerator BossCycle() {
        //Implemented in each specific class.
        yield return new WaitForSeconds(0);
    }

    protected virtual IEnumerator BossDeath() {
        //Implemented in each specific class.
        yield return new WaitForSeconds(0);
    }
}
