using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;
using SpaceShooter.Player;
using System;
using UnityEngine.UI;
using Unity.Netcode;

namespace SpaceShooter.Guns
{
    public class OneHandedGun : Gun, IPickableObject, IInteractableObject 
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private GunVisual _gunVisual;

        private PlayerItemPicker _currentItemPicker;

        private void Update()
        {
            UpdatePositionAndRotation();
        }

        public void Pick(PlayerItemPicker playerItemPicker)
        {
            if (!_currentItemPicker)
            {
                _currentItemPicker = playerItemPicker;
                _rigidbody.isKinematic = true;
                transform.parent = playerItemPicker.transform;
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

        private void UpdatePositionAndRotation()
        {
            if (_currentItemPicker)
            {
                transform.position = _currentItemPicker.transform.position;
                transform.rotation = _currentItemPicker.transform.rotation;
            }
        }

        protected override void UpdateVisualClientRpc()
        {
            _gunVisual.ReadyImage.color = _canShoot ? _gunVisual.ReadyColor : _gunVisual.NotReadyColor;
        }
    }

    [Serializable]
    public struct GunVisual
    {
        public Image ReadyImage;
        public Color ReadyColor;
        public Color NotReadyColor;
    }
}