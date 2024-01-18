using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] Vector3 ProjectileOffset;
    [SerializeField] int DetectionRange;
    [SerializeField] float ReloadSpeed;
    private float Proximity;
    private bool PlayerDetected;
    private bool Loaded;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ProjectilePrefab = Resources.Load<GameObject>("Prefabs/ProjectilePrefab");
        Loaded = true;
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
            Vector3 Target = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            gameObject.transform.LookAt(Target);
            LineOfSight();
            if (PlayerDetected && Loaded)
            {
                FireProjectile();
            }
        }
    }
    private void LineOfSight()
    {
        RaycastHit Hit;
        Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out Hit, DetectionRange);
        if (Hit.collider != null)
        {
            if (Hit.collider.gameObject.CompareTag("Player"))
            {
                PlayerDetected = true;
            }
        }
        else
        {
            PlayerDetected = false;
        }
    }
    private void FireProjectile()
    {
        Instantiate(ProjectilePrefab, transform.position + ProjectileOffset, Quaternion.identity);
        StartCoroutine("Reload");
    }
    private IEnumerator Reload()
    {
        Loaded = false;
        yield return new WaitForSeconds(ReloadSpeed);
        Loaded = true;
    }
}
