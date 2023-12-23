using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlamethrower : MonoBehaviour
{
    [SerializeField] float RateOfFire;
    [SerializeField] float ReloadSpeed;
    [SerializeField] int Range;
    [SerializeField] int MaxLoadedAmmo;
    [SerializeField] int MaxHeldAmmo;
    private bool RoFReset;
    private int CurrentLoadedAmmo;
    private int CurrentHeldAmmo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && RoFReset)
        {
            //Instantiate a flame particle effect. Insert script onto particles so that if an enemy gets hit by them, they deal damage, and one to disappear after a period of time.
        }
    }
}
