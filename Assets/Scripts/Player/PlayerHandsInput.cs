using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerHandsInput : MonoBehaviour
    {
        [SerializeField]
        private PlayerHand _leftHand;
        [SerializeField]
        public PlayerHand _rightHand;

        public void SetInputData(PlayerInputData playerInputData)
        {
            DoInputOperations(_leftHand, playerInputData.LeftStickPressed, playerInputData.LeftTriggerPressed);
            DoInputOperations(_rightHand, playerInputData.RightStickPressed, playerInputData.RightTriggerPressed);
        }

        private void DoInputOperations(PlayerHand hand, bool isPick, bool isInteract)
        {
            if (isInteract)
            {
                hand.InteractWithPickedItem();
            }
        }
    }
}