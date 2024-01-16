using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] PlayerHealth RespawnScript;
    [SerializeField] int PlayerLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            RespawnScript.Die();
        }
    }
}
