using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotgun : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Camera PlayerCam;
    [SerializeField] WeaponSwitch SwitchScript;
    [SerializeField] ParticleSystem ShotParticle;
    [SerializeField] float RateOfFire;
    [SerializeField] int WeaponDamage;
    [SerializeField] int Range;
    [SerializeField] int MaxLoadedAmmo;
    [SerializeField] int MaxHeldAmmo;
    private bool RoFReset;
    private int CurrentLoadedAmmo;
    private int CurrentHeldAmmo;
    void Start()
    {
        RoFReset = true;
        StartingAmmo();
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
        Instantiate(ShotParticle);
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
}
