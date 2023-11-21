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

        private void Start()
        {
            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Shutdown(bool boolValue)
        {
            _sceneLoader.LoadScene("MainMenu", ShutdownNetwork);
        }

        private void SubscribeOnEvents()
        {
            if (NetworkManager.Singleton)
            {
                NetworkManager.Singleton.OnClientStopped += Shutdown;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (NetworkManager.Singleton)
            {
                NetworkManager.Singleton.OnClientStopped -= Shutdown;
            }
        }

        private void ShutdownNetwork()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.Shutdown();
                Destroy(NetworkManager.Singleton.gameObject);
            }
        }
    }
}