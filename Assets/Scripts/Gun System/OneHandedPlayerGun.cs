using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;

namespace SpaceShooter.Guns
{
    public class OneHandedPlayerGun : Gun, IInteractableObject
    {
        public void Interact()
        {
            Shoot();
        }
    }
}