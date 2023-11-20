using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public struct PlayerBodyPartInformation
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public PlayerBodyPartInformation(Transform transform)
        {
            Position = transform.position;
            Rotation = transform.rotation;
        }
    }
}