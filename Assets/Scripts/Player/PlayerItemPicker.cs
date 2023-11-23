using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerItemPicker : NetworkBehaviour
    {
        public IPickableObject CurrentPickableObject { get; private set; }

        private List<IPickableObject> _pickableObjectsList = new List<IPickableObject>();

        public void PickOrDropAvailableObjects()
        {
            if(CurrentPickableObject != null)
            {
                CurrentPickableObject.Drop();
                CurrentPickableObject = null;
            }
            else if(_pickableObjectsList.Count > 0)
            {
                CurrentPickableObject = _pickableObjectsList[0];
                CurrentPickableObject.Pick(this);
            }
        }

        public void InteractWithPickedObject()
        {
            if(CurrentPickableObject != null)
            {
                CurrentPickableObject.Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ENTER IN TRIGGER");
            if(other.GetComponent<IPickableObject>() != null)
            {
                _pickableObjectsList.Add(other.GetComponent<IPickableObject>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IPickableObject>() != null)
            {
                _pickableObjectsList.Remove(other.GetComponent<IPickableObject>());
            }
        }
    }
}