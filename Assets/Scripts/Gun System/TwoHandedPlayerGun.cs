using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class TwoHandedPlayerGun : Gun
    {
        [SerializeField]
        private PlayerHand _corePlayerHand;

        [SerializeField]
        private PlayerHand _secondPlayerHand;

        private void Start()
        {
            SetGunToHand();
        }

        private void Update()
        {
            SetPositionAndRotationOfGun();
        }

        private void SetGunToHand()
        {
            if (_corePlayerHand != null)
            {
                _corePlayerHand.SetInteractableItem(this);
            }
        }

        private void SetPositionAndRotationOfGun()
        {
            transform.rotation = Quaternion.LookRotation(_secondPlayerHand.transform.position - _corePlayerHand.transform.position);
        }
    }
}