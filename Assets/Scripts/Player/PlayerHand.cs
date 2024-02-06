using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerHand : MonoBehaviour
    {
        public IInteractableObject CurrentInteractableObject { get; private set; }

        [SerializeField]
        private bool _hasInteractableObject;

        private void Update()
        {
            _hasInteractableObject = CurrentInteractableObject != null;
        }

        public void SetInteractableItem(IInteractableObject interactableObject)
        {
            CurrentInteractableObject = interactableObject;
        }

        public void ClearInteractableItem()
        {
            if (CurrentInteractableObject != null)
            {
                Destroy(CurrentInteractableObject.GameObject);
            }

            CurrentInteractableObject = null;
        }

        public void InteractWithPickedItem()
        {
            if (_hasInteractableObject)
            {
                CurrentInteractableObject.Interact();
            }
        }
    }
}