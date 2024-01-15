using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] Button RetryButton;
    [SerializeField] Button ExitButton;
    private void Start()
    {
        RetryButton.onClick.AddListener(Retry);
        ExitButton.onClick.AddListener(Exit);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
    public void Exit()
    {
        Application.Quit();
    }
}
