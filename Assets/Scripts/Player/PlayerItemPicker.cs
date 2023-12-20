using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerItemPicker : MonoBehaviour
    {
        public IPickableItem CurrentPickableItem { get; private set; }

        private List<IPickableItem> _pickableItemsList = new List<IPickableItem>();

        [SerializeField]
        private PlayerState _playerState;

        public void PickOrDropAvailableItems()
        {
            if (CurrentPickableItem != null)
            {
                CurrentPickableItem.Drop();
                CurrentPickableItem = null;
            }
            else if (_pickableItemsList.Count > 0)
            {
                if (!_pickableItemsList[0].IsPicked)
                {
                    CurrentPickableItem = _pickableItemsList[0];
                    CurrentPickableItem.Pick(transform);
                }
            }
        }

        public void InteractWithPickedItem()
        {
            if (CurrentPickableItem != null)
            {
                CurrentPickableItem.Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ENTER IN TRIGGER");
            if (other.GetComponent<IPickableItem>() != null)
            {
                _pickableItemsList.Add(other.GetComponent<IPickableItem>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IPickableItem>() != null)
            {
                _pickableItemsList.Remove(other.GetComponent<IPickableItem>());
            }
        }
    }
}