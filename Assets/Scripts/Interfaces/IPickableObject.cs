using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IPickableObject : IInteractableObject
    {
        public void Pick(PlayerItemPicker playerItemPicker);
        public void Drop();
    }
}