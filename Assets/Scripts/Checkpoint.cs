using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] RespawnManager RespawnScript;
    [SerializeField] int PlayerLayer;
    [SerializeField] Vector3 RespawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            RespawnScript.SetRespawn(RespawnPoint);
        }
    }
}
