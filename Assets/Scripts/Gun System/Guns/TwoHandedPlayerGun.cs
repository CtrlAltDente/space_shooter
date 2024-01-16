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

        public override void SetHands(PlayerHand coreHand, PlayerHand additionalHand = null)
        {
            _corePlayerHand = coreHand;
            _secondPlayerHand = additionalHand;

            coreHand.SetInteractableItem(this);
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
            if (_corePlayerHand != null && _secondPlayerHand != null)
            {
                transform.rotation = Quaternion.LookRotation(_secondPlayerHand.transform.position - _corePlayerHand.transform.position);
            }
        }
    }
}