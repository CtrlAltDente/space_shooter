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

        private void SetGunToHand()
        {
            if (_playerHand != null)
            {
                _playerHand.SetInteractableItem(this);
            }
        }
    }
}