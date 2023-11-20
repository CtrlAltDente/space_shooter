using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Network
{
    public class NetworkConnectionLogic : MonoBehaviour
    {
        public UnityEvent DoOnClientStartOperations;
        public UnityEvent DoOnHostStartOperations;

        private void Start()
        {
            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            if (NetworkManager.Singleton)
            {
                NetworkManager.Singleton.OnClientStarted += RaiseOnClientStartOperations;

                NetworkManager.Singleton.OnClientConnectedCallback += RaiseOnClientConnected;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (NetworkManager.Singleton)
            {
                NetworkManager.Singleton.OnClientStarted -= RaiseOnClientStartOperations;

                NetworkManager.Singleton.OnClientConnectedCallback -= RaiseOnClientConnected;
            }
        }

        private void RaiseOnClientStartOperations()
        {
            if (!NetworkManager.Singleton.IsHost && NetworkManager.Singleton.IsClient)
            {
                DoOnClientStartOperations?.Invoke();
            }
            else if (NetworkManager.Singleton.IsHost)
            {
                DoOnHostStartOperations?.Invoke();
            }
        }

        private void RaiseOnClientConnected(ulong clientId)
        {
            Debug.Log($"New client connected: {clientId}");
        }
    }
}