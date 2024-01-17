using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject EnemyCorpse;
    [SerializeField] AudioSource EnemyAudio;
    [SerializeField] AudioClip DamagedAudio;
    [SerializeField] ChasingEnemy ChasingBehaviour;
    [SerializeField] int EnemyType;
    [SerializeField] int MaxHP;
    [SerializeField] int CurrentHP;
    void Start()
    {
        EnemyAudio = GetComponent<AudioSource>();
        EnemyAudio.clip = DamagedAudio;
        CurrentHP = MaxHP;
        if (EnemyType == 0)
        {
            ChasingBehaviour = gameObject.GetComponent<ChasingEnemy>();
        }
    }

    public void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;
        PlayDamageSFX();
        if (CurrentHP <= 0)
        {
            Die();
        }
        if (EnemyType == 0)
        {
            ChasingBehaviour.Hit();
        }
    }

    private void PlayDamageSFX()
    {
        EnemyAudio.Play();
    }

    private void Die()
    {
        Instantiate(EnemyCorpse, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
