using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingParticle : MonoBehaviour
{
    public bool Boosted;
    [SerializeField] int ParticleDamage;
    [SerializeField] int BoostedDamage;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!Boosted)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(ParticleDamage);
            }
            else if (Boosted)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(BoostedDamage);
            }
        }
    }
}
