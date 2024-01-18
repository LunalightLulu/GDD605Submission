using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoosts : MonoBehaviour
{
    [SerializeField] PlayerHealth HPScript;
    [SerializeField] FirePistol PistolScript;
    [SerializeField] FireRifle RifleScript;
    [SerializeField] FireShotgun ShotgunScript;
    [SerializeField] StarterAssets.FirstPersonController PlayerController;
    [SerializeField] HUDTracker HUD;
    [SerializeField] string[] PickupTypes;
    [SerializeField] float BaseSpeed;
    [SerializeField] float BaseSprint;
    [SerializeField] float BoostedSpeed;
    [SerializeField] float BoostedSprint;
    [SerializeField] int SpeedBoostTime;
    [SerializeField] int DamageBoostTime;
    private int ActiveSpeedBonuses;
    private int ActiveDamageBonuses;

    private void Start()
    {
        HPScript = GetComponent<PlayerHealth>();
        PistolScript = GetComponentInChildren<FirePistol>(true);
        RifleScript = GetComponentInChildren<FireRifle>(true);
        ShotgunScript = GetComponentInChildren<FireShotgun>(true);
        PlayerController = GetComponent<StarterAssets.FirstPersonController>();
        BaseSpeed = PlayerController.MoveSpeed;
        BaseSprint = PlayerController.SprintSpeed;
    }
    public void PickupObtained(string Type)
    {
        if (Type == PickupTypes[0])
        {
            HealthPickup();
        }
        else if (Type == PickupTypes[1])
        {
            AmmoPickup();
        }
        else if (Type == PickupTypes[2])
        {
            SpeedPickup();
        }
        else if (Type == PickupTypes[3])
        {
            DamagePickup();
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
        PlayerController.MoveSpeed = BoostedSpeed;
        PlayerController.SprintSpeed = BoostedSprint;
        HUD.SpeedBoosted(true);
        ActiveSpeedBonuses++;
        StartCoroutine("ResetSpeed");
    }
    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(SpeedBoostTime);
        ActiveSpeedBonuses--;
        if (ActiveSpeedBonuses <= 0)
        {
            ActiveSpeedBonuses = 0;
            PlayerController.MoveSpeed = BaseSpeed;
            PlayerController.SprintSpeed = BaseSprint;
            HUD.SpeedBoosted(false);
        }
    }
    private void DamagePickup()
    {
        PistolScript.DamageBoost(true);
        RifleScript.DamageBoost(true);
        ShotgunScript.DamageBoost(true);
        HUD.DamageBoosted(true);
        ActiveDamageBonuses++;
        StartCoroutine("ResetDamage");
    }
    private IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(DamageBoostTime);
        ActiveDamageBonuses--;
        if (ActiveDamageBonuses <= 0)
        {
            ActiveDamageBonuses = 0;
            PistolScript.DamageBoost(false);
            RifleScript.DamageBoost(false);
            ShotgunScript.DamageBoost(false);
            HUD.DamageBoosted(false);
        }
    }
}
