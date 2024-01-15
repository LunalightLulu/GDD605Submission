using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FireRifle : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Camera PlayerCam;
    [SerializeField] CinemachineVirtualCamera CameraController;
    [SerializeField] WeaponSwitch SwitchScript;
    [SerializeField] ParticleSystem HitParticle;
    [SerializeField] ParticleSystem FireParticle;
    [SerializeField] Vector3 FireParticleOffset;
    [SerializeField] float RateOfFire;
    [SerializeField] int WeaponDamage;
    [SerializeField] int Range;
    [SerializeField] int UnzoomedFoV;
    [SerializeField] int ZoomedFoV;
    [SerializeField] int MaxLoadedAmmo;
    [SerializeField] int MaxHeldAmmo;
    private bool RoFReset;
    private bool Zoomed;
    private int CurrentLoadedAmmo;
    private int CurrentHeldAmmo;
    void Start()
    {
        HitParticle = Resources.Load<ParticleSystem>("Prefabs/HitParticle");
        FireParticle = Resources.Load<ParticleSystem>("Prefabs/FireParticleRifle");
        RoFReset = true;
        StartingAmmo();
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && RoFReset)
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
        if (Input.GetMouseButtonDown(1))
        {
            RifleZoom();
        }
    }
    private void Fire()
    {
        CreateFireParticles();
        CurrentLoadedAmmo--;
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
    }
    private void Sputter()
    {
        //Play a sfx.
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
    private void RifleZoom()
    {
        Zoomed = !Zoomed;
        if (Zoomed)
        {
            CameraController.m_Lens.FieldOfView = ZoomedFoV;
        }
        else if (!Zoomed)
        {
            CameraController.m_Lens.FieldOfView = UnzoomedFoV;
        }
    }
}
