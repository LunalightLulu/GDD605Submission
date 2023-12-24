using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePistol : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Camera PlayerCam;
    [SerializeField] ParticleSystem HitParticle;
    [SerializeField] ParticleSystem FireParticle;
    [SerializeField] Vector3 FireParticleOffset;
    [SerializeField] float RateOfFire;
    [SerializeField] float ReloadSpeed;
    [SerializeField] int WeaponDamage;
    [SerializeField] int Range;
    [SerializeField] int MaxLoadedAmmo;
    [SerializeField] int MaxHeldAmmo;
    private bool RoFReset;
    private int CurrentLoadedAmmo;
    private int CurrentHeldAmmo;
    void Start()
    {
        HitParticle = Resources.Load<ParticleSystem>("Prefabs/HitParticle");
        FireParticle = Resources.Load<ParticleSystem>("Prefabs/FireParticlePistol");
        RoFReset = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RoFReset)
        {
            HitCheck();
            StartCoroutine("ResetRoF");
        }
    }

    private void HitCheck()
    {
        CreateFireParticles();
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
        yield return new WaitForSeconds(RateOfFire);
        RoFReset = true;
    }
}
