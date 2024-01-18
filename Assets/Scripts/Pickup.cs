using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerBoosts ProcessScript;
    [SerializeField] string Type;
    [SerializeField] Vector3 RotationRate;
    [SerializeField] Vector3 FlotationRate;
    [SerializeField] Vector3 FlotationFluctuation;
    private Vector3 MaxHeight;
    private Vector3 MinHeight;
    private bool FloatUp;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ProcessScript = Player.GetComponent<PlayerBoosts>();
        MaxHeight = gameObject.transform.position + FlotationFluctuation;
        MinHeight = gameObject.transform.position - FlotationFluctuation;
    }
    private void Update()
    {
        gameObject.transform.Rotate(RotationRate);
        Float();
    }
    private void Float()
    {
        if (transform.position.y > MaxHeight.y)
        {
            FloatUp = false;
        }
        else if (transform.position.y < MinHeight.y)
        {
            FloatUp = true;
        }
        if (FloatUp)
        {
            transform.position += FlotationRate;
        }
        else if (!FloatUp)
        {
            transform.position -= FlotationRate;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ProcessScript.PickupObtained(Type);
            Destroy(gameObject);
        }
    }
}
