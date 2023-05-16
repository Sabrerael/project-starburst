using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {
    [SerializeField] List<GameObject> instructionTiles;
    [SerializeField] Button decreaseButton;
    [SerializeField] Button increaseButton;

    private int instructionIndex = 0;

    private void Start() {
        DisableButtons();
    }

    private void DisableButtons() {
        if (instructionIndex == 0) {
            decreaseButton.interactable = false;
        } else if (instructionIndex == 1) {
            decreaseButton.interactable = true;
        } else if (instructionIndex == instructionTiles.Count - 2) {
            increaseButton.interactable = true;
        } else if (instructionIndex == instructionTiles.Count - 1) {
            increaseButton.interactable = false;
        }
    }

    public void DecreaseIndex() {
        instructionTiles[instructionIndex].SetActive(false);
        instructionIndex--;
        instructionTiles[instructionIndex].SetActive(true);
        DisableButtons();
    }

    public void IncreaseIndex() {
        instructionTiles[instructionIndex].SetActive(false);
        instructionIndex++;
        instructionTiles[instructionIndex].SetActive(true);
        DisableButtons();
    }
}
