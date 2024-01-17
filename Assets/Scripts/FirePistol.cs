using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePistol : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Camera PlayerCam;
    [SerializeField] AudioSource PistolAudio;
    [SerializeField] WeaponSwitch SwitchScript;
    [SerializeField] HUDTracker HUD;
    [SerializeField] ParticleSystem HitParticle;
    [SerializeField] ParticleSystem FireParticle;
    [SerializeField] Vector3 FireParticleOffset;
    [SerializeField] AudioClip FireSFX;
    [SerializeField] AudioClip ReloadSFX;
    [SerializeField] AudioClip SputterSFX;
    [SerializeField] float RateOfFire;
    [SerializeField] int WeaponDamage;
    [SerializeField] int Range;
    [SerializeField] int MaxLoadedAmmo;
    [SerializeField] int MaxHeldAmmo;
    [SerializeField] int AmmoFromPickups;
    private bool RoFReset;
    private int CurrentLoadedAmmo;
    private int CurrentHeldAmmo;
    void Start()
    {
        PistolAudio = GetComponent<AudioSource>();
        HitParticle = Resources.Load<ParticleSystem>("Prefabs/HitParticle");
        FireParticle = Resources.Load<ParticleSystem>("Prefabs/FireParticlePistol");
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
        CreateFireParticles();
        PlayFireSFX();
        //Do a raycast, check if anything was hit. If it was, do a particle effect, and then if it was an enemy, deal damage.
        RaycastHit Hit;
        Physics.Raycast(Player.transform.position, PlayerCam.transform.forward, out Hit, Range);
        if (Hit.collider != null)
        {
            GameObject ThingHit = Hit.collider.gameObject;
            CreateHitParticles(Hit);
            if (ThingHit.CompareTag("Enemy"))
            {
                ThingHit.GetComponent<EnemyHealth>().TakeDamage(WeaponDamage);
            }
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
        PistolAudio.clip = FireSFX;
        PistolAudio.Play();
    }
    private void PlayReloadSFX()
    {
        PistolAudio.clip = ReloadSFX;
        PistolAudio.Play();
    }
    private void Sputter()
    {
        PistolAudio.clip = SputterSFX;
        PistolAudio.Play();
    }
    private void StartingAmmo()
    {
        CurrentLoadedAmmo = MaxLoadedAmmo;
        CurrentHeldAmmo = MaxHeldAmmo / 2;
    }
    private void CreateFireParticles()
    {
        Instantiate(FireParticle, transform.position + FireParticleOffset, Player.transform.rotation);
    }
    private void CreateHitParticles(RaycastHit Hit)
    {
        Instantiate(HitParticle, Hit.point, Quaternion.identity);
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
}
