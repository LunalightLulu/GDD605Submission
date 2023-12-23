using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRifle : MonoBehaviour
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
            //Do a raycast, check if anything was hit. If it was, do a particle effect, and then if it was an enemy, deal damage.
        }
    }
}
