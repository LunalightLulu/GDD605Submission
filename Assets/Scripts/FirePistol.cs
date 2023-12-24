using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePistol : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Camera PlayerCam;
    [SerializeField] ParticleSystem HitParticle;
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
        RoFReset = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RoFReset)
        {
            //Do a raycast, check if anything was hit. If it was, do a particle effect, and then if it was an enemy, deal damage.
            RaycastHit Hit;
            Physics.Raycast(Player.transform.position, PlayerCam.transform.forward, out Hit, Range);
            if (Hit.collider != null)
            {
                Debug.Log("Hit Something");
                GameObject ThingHit = Hit.collider.gameObject;
                Instantiate(HitParticle, Hit.point, Quaternion.identity);
                //Instantiate particle effect at hit location
                if (ThingHit.CompareTag("Enemy"))
                {
                    ThingHit.GetComponent<EnemyHealth>().TakeDamage(WeaponDamage);
                }
            }
            StartCoroutine("ResetRoF");
        }
    }

    private IEnumerator ResetRoF()
    {
        RoFReset = false;
        yield return new WaitForSeconds(RateOfFire);
        RoFReset = true;
    }
}
