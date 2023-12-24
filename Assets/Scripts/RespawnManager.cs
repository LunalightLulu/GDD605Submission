using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    void Start()
    {

    }

    public void TriggerRespawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
