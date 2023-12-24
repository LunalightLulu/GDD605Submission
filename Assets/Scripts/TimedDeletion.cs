using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeletion : MonoBehaviour
{
    [SerializeField] float TimeTillDeletion;
    void Start()
    {
        StartCoroutine("DeleteSelf");
        Debug.Log("I wake");
    }

    private IEnumerator DeleteSelf()
    {
        Debug.Log("I IEnumerate");
        yield return new WaitForSeconds(TimeTillDeletion);
        Destroy(gameObject);
        Debug.Log("Why am I alive");
    }
}
