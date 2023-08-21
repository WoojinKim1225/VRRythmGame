using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteJudgement : MonoBehaviour
{
    public float noteSpeed;
    public bool pressButtonThenDownEnable = false;
    public LineRenderer lineRenderer;
    public Vector3 rayStartPos, rayEndPos;
    public float maxDistance;
    public LayerMask hands;

    public float distance;

    public float destroyDistance = 0.01f;

    private void Start()
    {
        maxDistance = (rayEndPos - rayStartPos).magnitude;
    }

    private void Update() {
        transform.Translate(-Vector3.up * noteSpeed * Time.deltaTime);
        rayStartPos = transform.TransformPoint(lineRenderer.GetPosition(1));
        rayEndPos = transform.TransformPoint(lineRenderer.GetPosition(0));

        if (rayEndPos.z < 0.4f)
        {
            rayEndPos.z = 0.4f;
            lineRenderer.SetPosition(0, transform.InverseTransformPoint(rayEndPos));
        }
        maxDistance = (rayEndPos - rayStartPos).magnitude;
        if (Physics.Raycast(rayStartPos, -Vector3.forward, out RaycastHit hit, maxDistance, hands))
        {
            if (hit.transform.tag == transform.tag)
            {
                Debug.Log("asdfasdfd");
                distance = hit.distance;
                if (hit.distance < destroyDistance) Destroy(this.gameObject);
            }

        }
        if (rayStartPos.z < rayEndPos.z) Destroy(this.gameObject);
    }
}
