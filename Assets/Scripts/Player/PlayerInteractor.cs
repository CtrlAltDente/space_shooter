using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public PlayerHand LeftHand;
        public PlayerHand RightHand;

        public void SetInputData(PlayerInputData playerInputData)
        {
            DoInputOperations(LeftHand, playerInputData.LeftStickPressed, playerInputData.LeftTriggerPressed);
            DoInputOperations(RightHand, playerInputData.RightStickPressed, playerInputData.RightTriggerPressed);
        }

        private void DoInputOperations(PlayerHand hand, bool isPick, bool isInteract)
        {
            if (isPick)
            {
                hand.PickOrDropAvailableItems();
            }

            if(isInteract)
            {
                hand.InteractWithPickedItem();
            }
        }
    }
}