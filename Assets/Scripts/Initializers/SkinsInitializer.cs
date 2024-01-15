using SpaceShooter.Player;
using SpaceShooter.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class SkinsInitializer : MonoBehaviour
    {
        [SerializeField]
        private SkinsContainer _skinsContainer;
    }
}