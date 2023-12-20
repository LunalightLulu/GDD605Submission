using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 DefaultSpawn;
    [SerializeField] Vector3 RespawnPosition;
    void Start()
    {
        RespawnPosition = DefaultSpawn;
    }

    public void SetRespawn(Vector3 NewRespawn)
    {
        RespawnPosition = NewRespawn;
    }

    public void TriggerRespawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
