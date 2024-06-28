using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Button saveButton;

    /*private void Start() {
        if (saveButton.onClick.GetPersistentMethodName(0) ==  "SaveButton") {
            SettingsSaver settingsSaver = FindObjectOfType<SettingsSaver>();
            saveButton.onClick.AddListener(settingsSaver.SaveButton);
        }
    }*/

    public void OpenOptionsMenu() {
        gameObject.SetActive(true);
    }

    public void CloseOptionsMenu() {
        gameObject.SetActive(false);
    }
}
