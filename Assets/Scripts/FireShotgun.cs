using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotgun : MonoBehaviour
{
    [SerializeField] WeaponSwitch SwitchScript;
    [SerializeField] AmmoTracker Tracker;
    [SerializeField] ParticleSystem ShotParticle;
    [SerializeField] float RateOfFire;
    [SerializeField] int MaxLoadedAmmo;
    [SerializeField] int MaxHeldAmmo;
    private bool RoFReset;
    private int CurrentLoadedAmmo;
    private int CurrentHeldAmmo;
    void Start()
    {
        ShotParticle = Resources.Load<ParticleSystem>("Prefabs/ShotgunParticle");
        RoFReset = true;
        StartingAmmo();
        UpdateAmmoCount();
    }
    private void OnEnable()
    {
        UpdateAmmoCount();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RoFReset)
        {
            if (CurrentLoadedAmmo > 0)
            {
                Fire();
            }
            else if (CurrentHeldAmmo > 0)
            {
                Reload();
            }
            else
            {
                Sputter();
            }
            StartCoroutine("ResetRoF");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CurrentLoadedAmmo < MaxLoadedAmmo && CurrentHeldAmmo > 0)
            {
                Reload();
            }
        }
    }
    private void Fire()
    {
        CurrentLoadedAmmo--;
        UpdateAmmoCount();
        Instantiate(ShotParticle, gameObject.transform);
    }
    private void Reload()
    {
        if (CurrentHeldAmmo >= MaxLoadedAmmo - CurrentLoadedAmmo)
        {
            CurrentHeldAmmo -= MaxLoadedAmmo - CurrentLoadedAmmo;
            CurrentLoadedAmmo = MaxLoadedAmmo;
        }
        else if (CurrentHeldAmmo < MaxLoadedAmmo - CurrentLoadedAmmo)
        {
            CurrentLoadedAmmo += CurrentHeldAmmo;
            CurrentHeldAmmo = 0;
        }
        UpdateAmmoCount();
    }
    private void Sputter()
    {
        //Sfx
    }
    private void StartingAmmo()
    {
        CurrentLoadedAmmo = MaxLoadedAmmo;
        CurrentHeldAmmo = MaxHeldAmmo / 2;
    }
    private IEnumerator ResetRoF()
    {
        RoFReset = false;
        SwitchScript.DisableSwitch();
        yield return new WaitForSeconds(RateOfFire);
        RoFReset = true;
        SwitchScript.EnableSwitch();
    }
    private void UpdateAmmoCount()
    {
        Tracker.UpdateAmmoDisplay(CurrentLoadedAmmo, CurrentHeldAmmo);
    }
}
