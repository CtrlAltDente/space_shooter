using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReferences : MonoBehaviour
{
    public bool LeftTriggerPressed => LeftTriggerActionReference.action.ReadValue<bool>();
    public bool RightTriggerPressed => RightTriggerActionReference.action.ReadValue<bool>();

    [SerializeField]
    private InputActionReference LeftTriggerActionReference;
    [SerializeField]
    private InputActionReference RightTriggerActionReference;
}