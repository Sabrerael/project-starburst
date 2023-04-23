using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void OpenOptionsMenu() {
        gameObject.SetActive(true);
    }

    public void CloseOptionsMenu() {
        gameObject.SetActive(false);
    }
}
