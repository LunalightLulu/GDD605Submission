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
    [SerializeField] float AttackRange;
    [SerializeField] int AttackDamage;
    private float RealRotationRangeMax;
    private float RealRotationRangeMin;
    private bool Chasing;
    private bool RotateRight;
    NavMeshAgent Agent;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
            if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < AttackRange)
            {
                Attack();
            }
            else
            {
                Chase();
            }
            
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
        if (Agent.isStopped == true)
        {
            Agent.isStopped = false;
        }
        Vector3 Target = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        gameObject.transform.LookAt(Target);
        Agent.SetDestination(Player.transform.position);
    }
    private void Attack()
    {
        if (Agent.isStopped == false)
        {
            Agent.isStopped = true;
        }
        PlayerHP.TakeDamage(AttackDamage);
    }
    public void Hit()
    {
        Chasing = true;
    }
}
