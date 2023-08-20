using Steamworks;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour {
    [Header("General Properties")]
    [SerializeField] protected int scoreValue = 10000;
    [SerializeField] protected float timeBetweenPhases = 1.5f;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected AudioClip explosionSFX;

    protected Health health;
    protected GameObject deathParticles;
    protected bool currentStatsGood = false;
    private CallResult<UserStatsReceived_t> userStatsRecieved;
    private CallResult<UserStatsStored_t> userStatsStored;
    private CallResult<UserAchievementStored_t> userAchievementStored;

	private void OnEnable() {
		/*if (SteamManager.Initialized) {
			userStatsRecieved = CallResult<UserStatsReceived_t>.Create(OnUserStatsReceived);
            userStatsStored = CallResult<UserStatsStored_t>.Create(OnUserStatsStored);
            userAchievementStored = CallResult<UserAchievementStored_t>.Create(OnUserAchievementStored);
		}*/
	}

    private void Start() {
        currentStatsGood = SteamUserStats.RequestCurrentStats();
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

    protected void UnlockAchievement(string achievementName) {
        if (currentStatsGood) {
            Debug.Log("Attempting to unlock " + achievementName);
            var unlocked = SteamUserStats.SetAchievement(achievementName);
            var stored = SteamUserStats.StoreStats();
            if (unlocked && stored) {
                Debug.Log(achievementName + " should be unlocked");
            }
        }
    }

    private void OnUserStatsReceived(UserStatsReceived_t pCallback, bool bIOFailure) {
		if (pCallback.m_eResult != EResult.k_EResultOK || bIOFailure) {
			Debug.Log("There was an error retrieving User Stats, error# " + pCallback.m_eResult);
		}
		else {
			Debug.Log("User stats retrieved.");
		}
	}

    private void OnUserStatsStored(UserStatsStored_t pCallback, bool bIOFailure) {
		if (pCallback.m_eResult != EResult.k_EResultOK || bIOFailure) {
			Debug.Log("There was an error storing User Stats, error# " + pCallback.m_eResult);
		}
		else {
			Debug.Log("User stats stored.");
		}
	}

    private void OnUserAchievementStored(UserAchievementStored_t pCallback, bool bIOFailure) {
		if ((pCallback.m_nCurProgress != 0 && pCallback.m_nMaxProgress != 0) || bIOFailure) {
			Debug.Log("There was an error unlocking the Achievement: " + pCallback.m_rgchAchievementName);
		}
		else {
			Debug.Log("Achievement Unlocked.");
		}
	}
}
