using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTriggerValue : MonoBehaviour
{
    public bool isinTrigger;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<spawnCollider>().isInTrigger = true;
        isinTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<spawnCollider>().isInTrigger = false;
        isinTrigger = false;
    }
}
