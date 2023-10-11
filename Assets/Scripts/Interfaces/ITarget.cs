using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface ITarget
    {
        public bool IsLive { get; }

        public Transform Transform { get; }
    }
}