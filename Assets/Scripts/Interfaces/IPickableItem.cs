using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IPickableItem : IInteractableObject
    {
        public bool IsPicked { get; }

        public void Pick(Transform target);

        public void Drop();
    }
}