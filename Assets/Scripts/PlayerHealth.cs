using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int MaxHP;
    [SerializeField] int CurrentHP;
    [SerializeField] float IFrameTime;
    private bool Vulnerable;
    void Start()
    {
        CurrentHP = MaxHP;
        Vulnerable = true;
    }

    public void TakeDamage(int Damage)
    {
        if (Vulnerable)
        {
            CurrentHP -= Damage;
            StartCoroutine("IFrames");
            if (CurrentHP <= 0)
            {
                Die();
            }
        }
    }
    
    public void RestoreHP(int HP)
    {
        if (CurrentHP + HP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
        else
        {
            CurrentHP += HP;
        }
    }

    private void Die()
    {
        //Death screen.
        Destroy(gameObject);
    }
    private IEnumerator IFrames()
    {
        Vulnerable = false;
        yield return new WaitForSeconds(IFrameTime);
        Vulnerable = true;
    }
}
