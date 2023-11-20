using SpaceShooter.GameLogic;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace SpaceShooter.Network
{
    public class NetworkControl : MonoBehaviour
    {
        [SerializeField]
        private SceneLoader _sceneLoader;
        
        public void Shutdown()
        {
            _sceneLoader.LoadScene("MainMenu", ShutdownNetwork);
        }

        private void ShutdownNetwork()
        {
            NetworkManager.Singleton.Shutdown();
            Destroy(NetworkManager.Singleton.gameObject);
            _sceneLoader.LoadScene("MainMenu");
        }
    }
}