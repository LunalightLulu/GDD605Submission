using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LoadedDisplay;
    [SerializeField] TextMeshProUGUI TotalDisplay;

    public void UpdateAmmoDisplay(int Loaded, int Total)
    {
        LoadedDisplay.SetText(Loaded.ToString());
        TotalDisplay.SetText(Total.ToString());
        TotalDisplay.SetText(Total.ToString());
    }
}
