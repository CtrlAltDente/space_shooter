using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReferences : MonoBehaviour
{
    public bool LeftTriggerPressed => LeftTriggerActionReference.action.ReadValue<float>() > 0.5f;
    public bool RightTriggerPressed => RightTriggerActionReference.action.ReadValue<float>() > 0.5f;

    [SerializeField]
    private InputActionReference LeftTriggerActionReference;
    [SerializeField]
    private InputActionReference RightTriggerActionReference;
}