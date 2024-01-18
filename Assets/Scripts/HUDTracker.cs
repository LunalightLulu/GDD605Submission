using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlayerHP;
    [SerializeField] TextMeshProUGUI LoadedDisplay;
    [SerializeField] TextMeshProUGUI TotalDisplay;
    [SerializeField] GameObject AmmoTypeDisplay;
    [SerializeField] GameObject SpeedBoostIcon;
    [SerializeField] GameObject DamageBoostIcon;
    [SerializeField] Sprite[] AmmoSprite;
    private Image AmmoTypeImage;
    private void Start()
    {
        AmmoTypeImage = AmmoTypeDisplay.GetComponent<Image>();
    }
    public void UpdateAmmoDisplay(int Loaded, int Total)
    {
        LoadedDisplay.SetText(Loaded.ToString());
        TotalDisplay.SetText(Total.ToString());
    }
    public void UpdateHPDisplay(int NewHP)
    {
        PlayerHP.SetText(NewHP.ToString());
    }
    public void UpdateAmmoIcon(int AmmoType)
    {
        AmmoTypeImage.sprite = AmmoSprite[AmmoType];
    }
    public void SpeedBoosted(bool Active)
    {
        SpeedBoostIcon.SetActive(Active);
    }
    public void DamageBoosted(bool Active)
    {
        DamageBoostIcon.SetActive(Active);
    }
}
