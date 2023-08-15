using TMPro;
using UnityEngine;

public class WinScreenScore : MonoBehaviour {
    [SerializeField] TextMeshProUGUI scoreValue;
    [SerializeField] TextMeshProUGUI highscoreValue;
    [SerializeField] TextMeshProUGUI wavesSurvived;

    private void Start() {
        scoreValue.text = PlayerPrefs.GetInt("RECENT_SCORE", 0).ToString("000000000");
        
        if (wavesSurvived != null) {
            wavesSurvived.text = PlayerPrefs.GetInt("WAVES_SURVIVED", 0).ToString("00");
            highscoreValue.text = PlayerPrefs.GetInt("ENDLESS_HIGH_SCORE", 0).ToString("000000000");
        } else {
            highscoreValue.text = PlayerPrefs.GetInt("HIGH_SCORE", 0).ToString("000000000");
        }
    }
}
