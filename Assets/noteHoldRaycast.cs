using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteHoldRaycast : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3 rayStartPos, rayEndPos;
    private RaycastHit hitInfo;
    private Ray ray;
    public LayerMask handLayer;

    private void Update() {
        rayStartPos = transform.TransformPoint(lineRenderer.GetPosition(1));
        rayEndPos = transform.position;

        ray = new Ray(rayStartPos, rayEndPos - rayStartPos);

        if (Physics.Raycast(rayStartPos, -Vector3.forward, out hitInfo, (rayEndPos - rayStartPos).magnitude, handLayer)) {
            if (hitInfo.distance < 0.05f) Destroy(gameObject);
            lineRenderer.SetPosition(0, transform.InverseTransformPoint(rayStartPos - Vector3.forward * hitInfo.distance));
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(ray);
    }
}
