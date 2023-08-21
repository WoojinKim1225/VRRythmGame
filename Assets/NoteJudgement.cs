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
    public LayerMask layerMask;
    public float destroyDistance = 0.001f;
    public enum PressButton{ 
        Grip, 
        Trigger, 
        MainButton
    }
    public PressButton pressButton;

    private void Start()
    {
        maxDistance = (rayEndPos - rayStartPos).magnitude;
    }

    private void FixedUpdate() {
        transform.Translate(-Vector3.up * noteSpeed * Time.deltaTime);
        rayStartPos = transform.TransformPoint(lineRenderer.GetPosition(1));
        rayEndPos = transform.TransformPoint(lineRenderer.GetPosition(0));
        maxDistance = (rayEndPos - rayStartPos).magnitude;
        if (Physics.Raycast(rayStartPos, -Vector3.forward, out RaycastHit hit, maxDistance, layerMask))
        {
            Debug.Log("asdfasdfd");
            if (hit.distance < destroyDistance) Destroy(this.gameObject);
        }
    }
}
