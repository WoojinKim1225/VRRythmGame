using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVelocity : MonoBehaviour
{
    public Transform target;
    public Vector3 velocity;
    private Vector3 positionBefore = Vector3.zero;

    private void FixedUpdate() {
        transform.position = target.position;
        Vector3 position = transform.position;
        velocity = (position - positionBefore) / Time.fixedDeltaTime;
        positionBefore = position;
    }
}
