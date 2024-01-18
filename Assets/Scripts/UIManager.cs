using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject VictoryUI;
    [SerializeField] GameObject DeathUI;
    [SerializeField] GameObject HUDUI;
    public void PlayerDied()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        HUDUI.SetActive(false);
        DeathUI.SetActive(true);
    }
    public void Victory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        HUDUI.SetActive(false);
        VictoryUI.SetActive(true);
    }
}
