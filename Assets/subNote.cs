using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subNote : MonoBehaviour
{
    public Vector3 rayStartPos, rayEndPos;
    private RaycastHit hitInfo;
    private Ray ray;
    public LayerMask handLayer;
    public int value;
    private void Update() {
        rayStartPos = transform.position + Vector3.forward * 0.05f;
        rayEndPos = transform.position - Vector3.forward * 0.05f;

        ray = new Ray(rayStartPos, rayEndPos - rayStartPos);

        if (Physics.Raycast(rayStartPos, -Vector3.forward, out hitInfo, (rayEndPos - rayStartPos).magnitude, handLayer)) {
            if ((hitInfo.transform.gameObject.GetComponent<SetHandTag>().FingerTag & value) != 0) Destroy(gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(ray);
    }
}
