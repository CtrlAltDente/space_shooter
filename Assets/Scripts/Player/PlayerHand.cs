using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerHand : MonoBehaviour
    {
        public IPickableItem CurrentPickableItem { get; private set; }

        public void SetPickableItem(IPickableItem pickableItem)
        {
            CurrentPickableItem = pickableItem;
        }

        public void InteractWithPickedItem()
        {
            if (CurrentPickableItem != null)
            {
                CurrentPickableItem.Interact();
            }
        }
    }
}