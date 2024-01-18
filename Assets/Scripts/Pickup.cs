using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerBoosts ProcessScript;
    [SerializeField] string Type;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ProcessScript = Player.GetComponent<PlayerBoosts>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ProcessScript.PickupObtained(Type);
            Destroy(gameObject);
        }
    }
}
