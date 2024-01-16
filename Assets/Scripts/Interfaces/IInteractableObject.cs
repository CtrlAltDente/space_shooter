using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IInteractableObject
    {
        public GameObject GameObject { get; }

        public void Interact();
    }
}