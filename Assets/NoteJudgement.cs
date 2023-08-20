using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteJudgement : MonoBehaviour
{
    public float noteSpeed;
    public bool pressButtonThenDownEnable = false;
    public enum PressButton{ 
        Grip, 
        Trigger, 
        MainButton
    }
    public PressButton pressButton;

    private void FixedUpdate() {
        transform.Translate(-Vector3.up * noteSpeed * Time.deltaTime);
    }
}
