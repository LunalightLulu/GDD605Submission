using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] UIManager UI;
    [SerializeField] HUDTracker HUD;
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
            HUD.UpdateHPDisplay(CurrentHP);
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
            HUD.UpdateHPDisplay(CurrentHP);
        }
        else
        {
            CurrentHP += HP;
            HUD.UpdateHPDisplay(CurrentHP);
        }
    }

    public void Die()
    {
        UI.PlayerDied();
        gameObject.SetActive(false);
    }
    private IEnumerator IFrames()
    {
        Vulnerable = false;
        yield return new WaitForSeconds(IFrameTime);
        Vulnerable = true;
    }
}
