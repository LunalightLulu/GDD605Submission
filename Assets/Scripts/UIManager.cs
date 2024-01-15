using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject DeathUI;
    [SerializeField] GameObject HUDUI;
    public void PlayerDied()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        HUDUI.SetActive(false);
        DeathUI.SetActive(true);
    }
}
