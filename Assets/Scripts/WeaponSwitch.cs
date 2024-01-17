using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] HUDTracker HUD;
    [SerializeField] GameObject[] Weapons;
    private int CurrentWeapon;
    private bool CanSwitch;

    private void Start()
    {
        Weapons[0].SetActive(true);
        Invoke("UpdateInitialIcon", 0.1f);
        CanSwitch = true;
    }
    private void Update()
    {
        if (CanSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeWeapon(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeWeapon(2);
            }
            /*else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeWeapon(3);
            }*/
            else if (Input.mouseScrollDelta.y < 0)
            {
                int NewWeapon = CurrentWeapon - 1;
                if (NewWeapon < 0)
                {
                    NewWeapon = Weapons.Length - 1;
                }
                ChangeWeapon(NewWeapon);
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                int NewWeapon = CurrentWeapon + 1;
                if (NewWeapon > Weapons.Length - 1)
                {
                    NewWeapon = 0;
                }
                ChangeWeapon(NewWeapon);
            }
        }
    }

    private void ChangeWeapon(int NewWeapon)
    {
        Weapons[CurrentWeapon].SetActive(false);
        Weapons[NewWeapon].SetActive(true);
        CurrentWeapon = NewWeapon;
        HUD.UpdateAmmoIcon(CurrentWeapon);
    }
    public void EnableSwitch()
    {
        CanSwitch = true;
    }
    public void DisableSwitch()
    {
        CanSwitch = false;
    }
    private void UpdateInitialIcon()
    {
        HUD.UpdateAmmoIcon(0);
    }
}
