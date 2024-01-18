using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] Button RetryButton;
    [SerializeField] Button ExitButton;
    [SerializeField] string GameplayScene;
    private void Start()
    {
        RetryButton.onClick.AddListener(Retry);
        ExitButton.onClick.AddListener(Exit);
    }
    public void Retry()
    {
        SceneManager.LoadScene(GameplayScene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
