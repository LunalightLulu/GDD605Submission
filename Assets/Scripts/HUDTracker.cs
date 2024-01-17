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
}
