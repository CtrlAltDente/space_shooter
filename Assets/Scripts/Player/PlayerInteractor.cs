using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public PlayerItemPicker LeftHandItemPicker;
        public PlayerItemPicker RightHandItemPicker;

        public void SetInputData(PlayerInputData playerInputData)
        {
            InteractWithItem(LeftHandItemPicker, playerInputData.LeftStickPressed, playerInputData.LeftTriggerPressed);
            InteractWithItem(RightHandItemPicker, playerInputData.RightStickPressed, playerInputData.RightTriggerPressed);
        }

        private void InteractWithItem(PlayerItemPicker playerItemPicker, bool isPick, bool isInteract)
        {
            if(isPick)
            {
                playerItemPicker.PickOrDropAvailableObjects();
            }

            if(isInteract)
            {
                playerItemPicker.InteractWithPickedObject();
            }
        }
    }
}