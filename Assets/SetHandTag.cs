using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetHandTag : MonoBehaviour
{
    public int FingerTag = 0;
    
    public InputActionProperty thumb, index, middle;

    private void OnEnable() {
        thumb.action.Enable();
        index.action.Enable();
        middle.action.Enable();
    }

    private void OnDisable() {
        thumb.action.Disable();
        index.action.Disable();
        middle.action.Disable();
    }

    private void Update() {
        int a = thumb.action.ReadValue<float>() == 1 ? 1 : 0;
        int b = index.action.ReadValue<float>() == 1 ? 2 : 0;
        int c = middle.action.ReadValue<float>() == 1 ? 4 : 0;
        FingerTag = a + b + c;
    }
    


}
