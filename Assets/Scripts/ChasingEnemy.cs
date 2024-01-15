using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerHealth PlayerHP;
    [SerializeField] Vector3 RotationRate;
    [SerializeField] int LookRotationRange;
    [SerializeField] int DetectionRange;
    [SerializeField] int AttackRange;
    [SerializeField] int AttackDamage;
    private float RealRotationRangeMax;
    private float RealRotationRangeMin;
    private bool Chasing;
    private bool RotateRight;
    NavMeshAgent Agent;
    private void Start()
    {
        PlayerHP = Player.GetComponent<PlayerHealth>();
        Agent = gameObject.GetComponent<NavMeshAgent>();
        RealRotationRangeMax = gameObject.transform.eulerAngles.y + LookRotationRange;
        RealRotationRangeMin = gameObject.transform.eulerAngles.y - LookRotationRange;
    }
    private void Update()
    {
        if (!Chasing)
        {
            LookAround();
            Look();
        }
        else if (Chasing)
        {
            Chase();
        }
    }
    private void LookAround()
    {
        if (RotateRight)
        {
            gameObject.transform.Rotate(RotationRate);
        }
        else
        {
            gameObject.transform.Rotate(-RotationRate);
        }
        if (gameObject.transform.rotation.eulerAngles.y > RealRotationRangeMax || gameObject.transform.rotation.eulerAngles.y < RealRotationRangeMin)
        {
            RotateRight = !RotateRight;
        }
    }
    private void Look()
    {
        RaycastHit Hit;
        Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out Hit, DetectionRange);
        if (Hit.collider != null)
        {
            if (Hit.collider.gameObject.CompareTag("Player"))
            {
                Chasing = true;
            }
        }
    }
    private void Chase()
    {
        gameObject.transform.LookAt(Player.transform);
        Agent.SetDestination(Player.transform.position);
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < AttackRange)
        {
            Attack();
        }
    }
    private void Attack()
    {
        PlayerHP.TakeDamage(AttackDamage);
    }
}
