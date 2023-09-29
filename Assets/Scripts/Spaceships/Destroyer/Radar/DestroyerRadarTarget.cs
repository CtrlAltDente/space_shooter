using SpaceShooter.Spaceships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.UI.Destroyer
{
    public class DestroyerRadarTarget : MonoBehaviour
    {
        public DestroyerStatus Target;

        public Vector2 CurrentVector;
        public bool IsTargetLive => Target.IsLive;

        public void SetTarget(DestroyerStatus destroyer)
        {
            Target = destroyer;
        }
    }
}