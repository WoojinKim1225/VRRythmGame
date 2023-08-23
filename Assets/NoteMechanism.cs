using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMechanism : MonoBehaviour
{
    public GameObject noteStart, noteEnd;
    public LineRenderer lineRenderer;
    private float distancePerMilliSeconds = 0;
    private float qpms = 50000;
    private new Rigidbody rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnStart(bool value, float distancePerMilliSeconds, float qpms) {
        this.distancePerMilliSeconds = distancePerMilliSeconds;
        this.qpms = qpms;
        noteStart.transform.localPosition = lineRenderer.GetPosition(0);

        if (value) {
            Vector3 endPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            noteEnd.transform.localPosition = endPos.x * Vector3.right + endPos.y * Vector3.forward;
        } else {
            noteEnd.SetActive(false);
            lineRenderer.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate() {
        rigidbody.MovePosition(transform.position - Vector3.forward * distancePerMilliSeconds * 1000f * 500000f / qpms * Time.deltaTime);
    }
}
