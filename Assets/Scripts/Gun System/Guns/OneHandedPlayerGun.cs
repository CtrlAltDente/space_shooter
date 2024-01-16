using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;
using SpaceShooter.Player;

namespace SpaceShooter.Guns
{
    public class OneHandedPlayerGun : Gun
    {
        [SerializeField]
        private PlayerHand _playerHand;

        private void Start()
        {
            SetGunToHand();
        }

        public override void SetHands(PlayerHand coreHand, PlayerHand additionalHand = null)
        {
            _playerHand = coreHand;

            coreHand.SetInteractableItem(this);
        }

        private void SetGunToHand()
        {
            if (_playerHand != null)
            {
                _playerHand.SetInteractableItem(this);
            }
        }
    }
}