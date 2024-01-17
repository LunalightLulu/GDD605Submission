using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeletion : MonoBehaviour
{
    [SerializeField] float TimeTillDeletion;
    void Start()
    {
        StartCoroutine("DeleteSelf");
    }

    private IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(TimeTillDeletion);
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
