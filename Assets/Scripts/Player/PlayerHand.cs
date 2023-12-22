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

        public void SetInteractableItem(IInteractableObject interactableObject)
        {
            CurrentInteractableObject = interactableObject;
        }

        public void InteractWithPickedItem()
        {
            if (CurrentInteractableObject != null)
            {
                CurrentInteractableObject.Interact();
            }
        }
    }
}