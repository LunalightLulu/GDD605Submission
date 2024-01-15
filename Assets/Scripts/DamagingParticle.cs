using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingParticle : MonoBehaviour
{
    [SerializeField] int ParticleDamage;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("Hit");
            other.GetComponent<EnemyHealth>().TakeDamage(ParticleDamage);
        }
    }
}
