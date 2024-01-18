using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotgun : MonoBehaviour
{
    [SerializeField] AudioSource ShotgunAudio;
    [SerializeField] WeaponSwitch SwitchScript;
    [SerializeField] HUDTracker HUD;
    [SerializeField] ParticleSystem ShotParticle;
    [SerializeField] AudioClip FireSFX;
    [SerializeField] AudioClip ReloadSFX;
    [SerializeField] AudioClip SputterSFX;
    [SerializeField] float RateOfFire;
    [SerializeField] int MaxLoadedAmmo;
    [SerializeField] int MaxHeldAmmo;
    [SerializeField] int AmmoFromPickups;
    private bool DamageBoosted;
    private bool RoFReset;
    private int CurrentLoadedAmmo;
    private int CurrentHeldAmmo;
    void Start()
    {
        ShotgunAudio = GetComponent<AudioSource>();
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
        PlayFireSFX();
        var Shot = Instantiate(ShotParticle, gameObject.transform);
        if (DamageBoosted)
        {
            Shot.GetComponent<DamagingParticle>().Boosted = true;
        }
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
        PlayReloadSFX();
        UpdateAmmoCount();
    }
    private void PlayFireSFX()
    {
        ShotgunAudio.clip = FireSFX;
        ShotgunAudio.Play();
    }
    private void PlayReloadSFX()
    {
        ShotgunAudio.clip = ReloadSFX;
        ShotgunAudio.Play();
    }
    private void Sputter()
    {
        ShotgunAudio.clip = SputterSFX;
        ShotgunAudio.Play();
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
        HUD.UpdateAmmoDisplay(CurrentLoadedAmmo, CurrentHeldAmmo);
    }
    public void AmmoPickup()
    {
        if (CurrentHeldAmmo + AmmoFromPickups > MaxHeldAmmo)
        {
            CurrentHeldAmmo = MaxHeldAmmo;
        }
        else
        {
            CurrentHeldAmmo += AmmoFromPickups;
        }
        if (gameObject.activeInHierarchy == true)
        {
            UpdateAmmoCount();
        }
    }
    public void DamageBoost(bool Active)
    {
        DamageBoosted = Active;
    }
}
