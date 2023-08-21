using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCollider : MonoBehaviour
{
    public bool isInTrigger;
    public GameObject noteCollider;
    public Transform pianoKeyOffset;

    private void FixedUpdate()
    {
        if (isInTrigger)
        {
            noteCollider.SetActive(true);
            noteCollider.transform.position = transform.position.x * Vector3.right + pianoKeyOffset.position;
        } else
        {
            noteCollider.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
    }
}
