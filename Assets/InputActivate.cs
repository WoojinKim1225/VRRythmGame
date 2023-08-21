using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActivate : MonoBehaviour
{
    public InputActionProperty activate;
    public float value = 0, oldvalue = 0;
    private BoxCollider collider;
    private MeshRenderer meshRenderer;
    public bool isPressed = false;

    private void OnEnable()
    {
        activate.action.Enable();
    }
    private void OnDisable()
    {
        activate.action.Disable();
    }
    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        value = activate.action.ReadValue<float>();
        if (value == 1 && oldvalue == 0)
        {
            StartCoroutine(PressAction());
        }
        collider.enabled = (value == 1);
        meshRenderer.enabled = (value == 1);
        oldvalue = value;
    }

    private IEnumerator PressAction()
    {
        isPressed = true;
        yield return new WaitForSeconds(0.2f);
        isPressed = false;
        yield return null;
    }
}
