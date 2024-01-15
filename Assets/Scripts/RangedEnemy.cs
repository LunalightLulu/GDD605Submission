using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] int DetectionRange;
    private float Proximity;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Detect();
    }
    private void Detect()
    {
        Proximity = Vector3.Distance(transform.position, Player.transform.position);
        if (Proximity < DetectionRange)
        {
            RaycastHit Hit;
            Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out Hit, DetectionRange);
            //Check if Raycast hits player, if it does, then become Alert and shoot that fucker.
        }
    }
}
