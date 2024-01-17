using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerHealth HPScript;
    [SerializeField] FirePistol PistolScript;
    [SerializeField] FireRifle RifleScript;
    [SerializeField] FireShotgun ShotgunScript;
    [SerializeField] StarterAssets.FirstPersonController PlayerController;
    [SerializeField] PickupType Type;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        HPScript = Player.GetComponent<PlayerHealth>();
        PistolScript = Player.GetComponentInChildren<FirePistol>(true);
        RifleScript = Player.GetComponentInChildren<FireRifle>(true);
        ShotgunScript = Player.GetComponentInChildren<FireShotgun>(true);
        PlayerController = Player.GetComponent<StarterAssets.FirstPersonController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Type == PickupType.Health)
            {
                HealthPickup();
            }
            else if (Type == PickupType.Ammo)
            {
                AmmoPickup();
            }
            else if (Type == PickupType.Speed)
            {
                SpeedPickup();
            }
            else if (Type == PickupType.Damage)
            {
                DamagePickup();
            }
            Destroy(gameObject);
        }
    }

    private void HealthPickup()
    {
        HPScript.RestoreHP();
    }
    private void AmmoPickup()
    {
        PistolScript.AmmoPickup();
        RifleScript.AmmoPickup();
        ShotgunScript.AmmoPickup();
    }
    private void SpeedPickup()
    {

    }
    private void DamagePickup()
    {

    }
}
