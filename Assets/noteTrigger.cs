using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteTrigger : MonoBehaviour
{
    public Vector3 rayStartPos, rayEndPos;
    private RaycastHit hitInfo;
    private Ray ray;
    public float rayDistance;
    public LayerMask handLayer;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == handLayer) {
            Vector3 v = other.gameObject.GetComponent<GetVelocity>().velocity;
            Debug.Log(v);
            if (Vector3.Dot(v, transform.forward) > 0) {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void FixedUpdate() {
        rayStartPos = transform.position + transform.forward * rayDistance;
        rayEndPos = transform.position;

        ray = new Ray(rayStartPos, -transform.forward * rayDistance);

        if (Physics.Raycast(ray, out hitInfo, rayDistance, handLayer)) {
            Destroy(transform.parent.gameObject);
        }

    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(ray);
    }
}
