using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReferences : MonoBehaviour
{
    public bool LeftTriggerPressed => LeftTriggerActionReference.action.triggered;
    public bool RightTriggerPressed => RightTriggerActionReference.action.triggered;

    public bool LeftStickPressed => LeftStickActionReference.action.triggered;
    public bool RightStickPressed => RightStickActionReference.action.triggered;

    [SerializeField]
    private InputActionReference LeftTriggerActionReference;
    [SerializeField]
    private InputActionReference RightTriggerActionReference;

    [SerializeField]
    private InputActionReference LeftStickActionReference;
    [SerializeField]
    private InputActionReference RightStickActionReference;
}