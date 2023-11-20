using SpaceShooter.GameLogic;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Network
{
    public class SessionControl : MonoBehaviour
    {
        [SerializeField]
        private SceneLoader _sceneLoader;

        [ClientRpc]
        public void StartGameForSessionClientRpc()
        {
            _sceneLoader.LoadNetworkScene("Player");
        }
    }
}