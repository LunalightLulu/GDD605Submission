using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int EnemyType;
    [SerializeField] ChasingEnemy ChasingBehaviour;
    [SerializeField] int MaxHP;
    [SerializeField] int CurrentHP;
    void Start()
    {
        CurrentHP = MaxHP;
        if (EnemyType == 0)
        {
            ChasingBehaviour = gameObject.GetComponent<ChasingEnemy>();
        }
    }

    public void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;
        if (CurrentHP <= 0)
        {
            Die();
        }
        if (EnemyType == 0)
        {
            ChasingBehaviour.Hit();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
