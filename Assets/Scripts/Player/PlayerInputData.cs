using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public struct PlayerInputData
    {
        public bool LeftTriggerPressed;
        public bool RightTriggerPressed;

        public PlayerInputData(bool leftTriggerPressed, bool rightTriggerPressed)
        {
            LeftTriggerPressed = leftTriggerPressed;
            RightTriggerPressed = rightTriggerPressed;
        }
    }
}