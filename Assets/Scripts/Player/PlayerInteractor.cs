using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public PlayerItemPicker LeftHandPicker;
        public PlayerItemPicker RightHandPicker;

        public void SetInputData(PlayerInputData playerInputData)
        {
            InteractWithItem(LeftHandPicker, playerInputData.LeftStickPressed, playerInputData.LeftTriggerPressed);
            InteractWithItem(RightHandPicker, playerInputData.RightStickPressed, playerInputData.RightTriggerPressed);
        }

        private void InteractWithItem(PlayerItemPicker picker, bool isPick, bool isInteract)
        {
            if (isPick)
            {
                picker.PickOrDropAvailableObjects();
            }

            if(isInteract)
            {
                picker.InteractWithPickedObject();
            }
        }
    }
}