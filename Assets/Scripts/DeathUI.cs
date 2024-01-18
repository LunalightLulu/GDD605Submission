using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] string GameplayScene;
    public void Retry()
    {
        SceneManager.LoadScene(GameplayScene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
