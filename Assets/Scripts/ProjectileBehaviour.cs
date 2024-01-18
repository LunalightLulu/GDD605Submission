using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 TargetOffset;
    [SerializeField] float ProjectileSpeed;
    [SerializeField] int ProjectileDamage;
    private PlayerHealth PlayerHP;
    private Vector3 TargetPosition;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHP = Player.GetComponent<PlayerHealth>();
        TargetPosition = (((Player.transform.position + TargetOffset) - transform.position) * 10) + Player.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, ProjectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHP.TakeDamage(ProjectileDamage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}
