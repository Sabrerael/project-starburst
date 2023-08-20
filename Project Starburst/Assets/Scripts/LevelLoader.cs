using Steamworks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] GameObject eolPlayerClone;

	private CallResult<LeaderboardFindResult_t> steamLeaderboard;
    private CallResult<LeaderboardFindResult_t> wavesLeaderboard;
    private CallResult<LeaderboardFindResult_t> endlessLeaderboard;
    private CallResult<LeaderboardScoreUploaded_t> scoreUploaded;
    private SteamLeaderboard_t leaderboard;
    private SteamLeaderboard_t w_Leaderboard;
    private SteamLeaderboard_t e_Leaderboard;

    protected bool currentStatsGood = false;
    private CallResult<UserStatsReceived_t> userStatsRecieved;
    private CallResult<UserStatsStored_t> userStatsStored;
    private CallResult<UserAchievementStored_t> userAchievementStored;

	private void OnEnable() {
		if (SteamManager.Initialized) {
			steamLeaderboard = CallResult<LeaderboardFindResult_t>.Create(OnSteamLeaderboard);
            wavesLeaderboard = CallResult<LeaderboardFindResult_t>.Create(OnWavesLeaderboard);
            endlessLeaderboard = CallResult<LeaderboardFindResult_t>.Create(OnEndlessLeaderboard);
            scoreUploaded = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderboardScoreUploaded);
            userStatsRecieved = CallResult<UserStatsReceived_t>.Create(OnUserStatsReceived);
            userStatsStored = CallResult<UserStatsStored_t>.Create(OnUserStatsStored);
            userAchievementStored = CallResult<UserAchievementStored_t>.Create(OnUserAchievementStored);
		}
	}

    private void Start() {
        SteamAPICall_t handle = SteamUserStats.FindLeaderboard("High Score");
		steamLeaderboard.Set(handle);
        handle = SteamUserStats.FindLeaderboard("Endless Mode Waves Survived");
        wavesLeaderboard.Set(handle);
        handle = SteamUserStats.FindLeaderboard("Endless Mode High Score");
        endlessLeaderboard.Set(handle);
        currentStatsGood = SteamUserStats.RequestCurrentStats();
    }

    private void DestroyPlayerObjects() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            Player playerComp = player.GetComponent<Player>();
            var wavesSurvived = playerComp.GetWavesSurvived();
            
            if (SteamManager.Initialized) {
                if (wavesSurvived == 0) {
                    var score = playerComp.GetTotalScore();
                    var highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
                    PlayerPrefs.SetInt("RECENT_SCORE", score);
                    PlayerPrefs.SetInt("HIGH_SCORE", Mathf.Max(score, highScore));
                    SteamAPICall_t handle = SteamUserStats.UploadLeaderboardScore(leaderboard,
                                                                    ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest,
                                                                    score,
                                                                    null,
                                                                    0);
                    scoreUploaded.Set(handle);
                    Debug.Log("Called UploadLeaderboardScore");
                } else {
                    PlayerPrefs.SetInt("WAVES_SURVIVED", wavesSurvived);
                    var score = playerComp.GetTotalScore();
                    var highScore = PlayerPrefs.GetInt("ENDLESS_HIGH_SCORE", 0);
                    PlayerPrefs.SetInt("RECENT_SCORE", score);
                    PlayerPrefs.SetInt("ENDLESS_HIGH_SCORE", Mathf.Max(score, highScore));
                    SteamAPICall_t handle = SteamUserStats.UploadLeaderboardScore(w_Leaderboard,
                                                                    ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest,
                                                                    wavesSurvived,
                                                                    null,
                                                                    0);
                    scoreUploaded.Set(handle);
                    Debug.Log("Called UploadLeaderboardScore");

                    handle = SteamUserStats.UploadLeaderboardScore(e_Leaderboard,
                                                                    ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest,
                                                                    score,
                                                                    null,
                                                                    0);
                    scoreUploaded.Set(handle);
                    Debug.Log("Called UploadLeaderboardScore");

                    if (currentStatsGood) {
                        Debug.Log("Attempting to unlock PLAY_ENDLESS_MODE");
                        var unlocked = SteamUserStats.SetAchievement("PLAY_ENDLESS_MODE");
                        var stored = SteamUserStats.StoreStats();
                        if (unlocked && stored) {
                            Debug.Log("PLAY_ENDLESS_MODE should be unlocked");
                        }
                    }
                }
            }
            GameObject.Destroy(GameObject.FindObjectOfType<PauseMenu>(true).gameObject);
            GameObject.Destroy(GameObject.Find("HUD"));
            GameObject.Destroy(player);
        }
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0));
        DestroyPlayerObjects();
    }

    public void LoadOptions() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadCredits() {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadLevelOne() {
        StartCoroutine(LoadLevel(3));
    }

    public void LoadLevelTwo() {
        StartCoroutine(WinCelebration(4));
    }

    public void LoadLevelThree() {
        StartCoroutine(WinCelebration(5));
    }

    public void LoadLevelFour() {
        StartCoroutine(WinCelebration(6));
    }

    public void LoadLevelFive() {
        StartCoroutine(WinCelebration(7));
    }

    public void LoadLevelSix() {
        StartCoroutine(WinCelebration(8));
    }

    public void LoadLevelSeven() {
        StartCoroutine(WinCelebration(9));
    }

    public void LoadEndlessMode() {
        StartCoroutine(LoadLevel(12));
    }

    public void LoadWinScreen() {
        StartCoroutine(WinCelebration(10));
    }

    public void LoadGameOver() {
        DestroyPlayerObjects();
        StartCoroutine(LoadLevel(11));
    }

    public void LoadGameOverEndlessMode() {
        DestroyPlayerObjects();
        StartCoroutine(LoadLevel(13));
    }

    public void QuitGame() {
        SteamAPI.Shutdown();
        Application.Quit();
    }

	private void OnSteamLeaderboard(LeaderboardFindResult_t pCallback, bool bIOFailure) {
		if (pCallback.m_bLeaderboardFound != 1 || bIOFailure) {
			Debug.Log("There was an error retrieving the Steam Leaderboard.");
		}
		else {
			Debug.Log("Steam Leaderboard name: " + pCallback.m_hSteamLeaderboard);
            leaderboard = pCallback.m_hSteamLeaderboard;
		}
	}

    private void OnWavesLeaderboard(LeaderboardFindResult_t pCallback, bool bIOFailure) {
		if (pCallback.m_bLeaderboardFound != 1 || bIOFailure) {
			Debug.Log("There was an error retrieving the Steam Leaderboard.");
		}
		else {
			Debug.Log("Steam Leaderboard name: " + pCallback.m_hSteamLeaderboard);
            w_Leaderboard = pCallback.m_hSteamLeaderboard;
		}
	}

    private void OnEndlessLeaderboard(LeaderboardFindResult_t pCallback, bool bIOFailure) {
		if (pCallback.m_bLeaderboardFound != 1 || bIOFailure) {
			Debug.Log("There was an error retrieving the Steam Leaderboard.");
		}
		else {
			Debug.Log("Steam Leaderboard name: " + pCallback.m_hSteamLeaderboard);
            e_Leaderboard = pCallback.m_hSteamLeaderboard;
		}
	}

    private void OnLeaderboardScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure) {
		if (pCallback.m_bSuccess != 1 || bIOFailure) {
			Debug.Log("There was an error uploading to the Steam Leaderboard.");
		}
		else {
			Debug.Log("Score of " + pCallback.m_nScore + " uploaded to " + pCallback.m_hSteamLeaderboard + " leaderboard");
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

    private IEnumerator LoadLevel(int sceneIndex) {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator WinCelebration(int levelIndex) {
        GameObject player = GameObject.Find("Player");
        player.transform.GetChild(1).GetComponent<BulletMagnet>().ClearBulletArray();
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        player.transform.GetChild(3).gameObject.SetActive(false);
        player.transform.GetChild(4).gameObject.SetActive(false);
        player.transform.GetChild(5).gameObject.SetActive(false);
        player.transform.GetChild(6).gameObject.SetActive(false);
        player.transform.GetChild(7).gameObject.SetActive(false);
        GameObject playerClone = Instantiate(eolPlayerClone, new Vector3(0, -1, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        playerClone.GetComponent<Animator>().SetTrigger("Fly");
        yield return new WaitForSeconds(2);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        if (levelIndex == 4) { DestroyPlayerObjects(); }
    }
}