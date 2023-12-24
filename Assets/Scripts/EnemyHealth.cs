using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHP;
    [SerializeField] int CurrentHP;
    void Start()
    {
        CurrentHP = MaxHP;
    }

    public void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;
        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Instantiate a particle effect splash thingy.
        Destroy(gameObject);
        //Do score things probably.
    }
}
