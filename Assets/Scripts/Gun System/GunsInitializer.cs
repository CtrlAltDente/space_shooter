using SpaceShooter.Guns;
using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class GunsInitializer : NetworkBehaviour
    {
        [SerializeField]
        private PlayerHand _rightHand;
        [SerializeField]
        private PlayerHand _leftHand;

        [SerializeField]
        private GunsPreset[] _gunsPresets;

        public void InitializeGuns(int gunIndex)
        {
            EnableGun(_rightHand, _gunsPresets[gunIndex].Guns[0]);
            EnableGun(_leftHand, _gunsPresets[gunIndex].Guns[1]);
        }

        private void EnableGun(PlayerHand playerHand, Gun gun)
        {
            if (gun != null)
            {
                gun.gameObject.SetActive(true);

                playerHand.SetInteractableItem(gun);
            }
        }
    }
}