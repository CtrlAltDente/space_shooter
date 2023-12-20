using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public PlayerItemPicker LeftHandPicker;
        public PlayerItemPicker RightHandPicker;

        public void SetInputData(PlayerInputData playerInputData)
        {
            DoInputOperations(LeftHandPicker, playerInputData.LeftStickPressed, playerInputData.LeftTriggerPressed);
            DoInputOperations(RightHandPicker, playerInputData.RightStickPressed, playerInputData.RightTriggerPressed);
        }

        private void DoInputOperations(PlayerItemPicker picker, bool isPick, bool isInteract)
        {
            if (isPick)
            {
                picker.PickOrDropAvailableItems();
            }

            if(isInteract)
            {
                picker.InteractWithPickedItem();
            }
        }
    }
}