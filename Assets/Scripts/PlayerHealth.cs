using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] UIManager UI;
    [SerializeField] HUDTracker HUD;
    [SerializeField] AudioSource PlayerAudio;
    [SerializeField] AudioClip[] DamageSounds;
    [SerializeField] int MaxHP;
    [SerializeField] int CurrentHP;
    [SerializeField] int HPFromPickups;
    [SerializeField] float IFrameTime;
    private bool Vulnerable;
    void Start()
    {
        PlayerAudio = GetComponent<AudioSource>();
        CurrentHP = MaxHP;
        Vulnerable = true;
    }

    public void TakeDamage(int Damage)
    {
        if (Vulnerable)
        {
            CurrentHP -= Damage;
            HUD.UpdateHPDisplay(CurrentHP);
            PlayDamageAudio();
            StartCoroutine("IFrames");
            if (CurrentHP <= 0)
            {
                Die();
            }
        }
    }

    private void PlayDamageAudio()
    {
        int PickClip = Random.Range(0, DamageSounds.Length);
        PlayerAudio.clip = DamageSounds[PickClip];
        PlayerAudio.Play();
    }
    
    public void RestoreHP()
    {
        if (CurrentHP + HPFromPickups > MaxHP)
        {
            CurrentHP = MaxHP;
            HUD.UpdateHPDisplay(CurrentHP);
        }
        else
        {
            CurrentHP += HPFromPickups;
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
