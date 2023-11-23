using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public OneHandedPlayerGun LeftHandGun;
        public OneHandedPlayerGun RightHandGun;

        public void SetInputData(PlayerInputData playerInputData)
        {
            InteractWithItem(LeftHandGun, playerInputData.LeftStickPressed, playerInputData.LeftTriggerPressed);
            InteractWithItem(RightHandGun, playerInputData.RightStickPressed, playerInputData.RightTriggerPressed);
        }

        private void InteractWithItem(OneHandedPlayerGun playerGun, bool isPick, bool isInteract)
        {
            if (isInteract)
            {
                playerGun.Interact();
            }
        }
    }
}