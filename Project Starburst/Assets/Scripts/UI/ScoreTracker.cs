using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour {
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI scoreValue;

    private void Update() {
        scoreValue.text = player.GetTotalScore().ToString();
    }
}
