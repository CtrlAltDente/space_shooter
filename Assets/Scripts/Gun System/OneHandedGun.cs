using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;
using SpaceShooter.Player;

namespace SpaceShooter.Guns
{
    public class OneHandedGun : Gun, IPickableObject, IInteractableObject 
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        private PlayerItemPicker _currentItemPicker;

        public void Pick(PlayerItemPicker playerItemPicker)
        {
            if (!_currentItemPicker)
            {
                _currentItemPicker = playerItemPicker;
                _rigidbody.isKinematic = true;
                transform.parent = playerItemPicker.transform;
                transform.position = playerItemPicker.transform.position;
                transform.rotation = playerItemPicker.transform.rotation;
            }
        }

        public void Drop()
        {
            _currentItemPicker = null;
            transform.parent = null;
            _rigidbody.isKinematic = false;
        }

        public void Interact()
        {
            Shoot();
        }
    }
}