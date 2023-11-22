using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Network
{
    public class NetworkConnectionLogic : MonoBehaviour
    {
        public UnityEvent DoOnClientStartOperations;
        public UnityEvent DoOnHostStartOperations;

        public UnityEvent OnClientConnectedOrDisconnectedEvent;

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

                NetworkManager.Singleton.ConnectionApprovalCallback += ApproveConnection;

                NetworkManager.Singleton.OnClientConnectedCallback += RaiseOnClientConnectedOrDisconnectedEvent;
                NetworkManager.Singleton.OnClientDisconnectCallback += RaiseOnClientConnectedOrDisconnectedEvent;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (NetworkManager.Singleton)
            {
                NetworkManager.Singleton.OnClientStarted -= RaiseOnClientStartOperations;

                NetworkManager.Singleton.OnClientConnectedCallback -= RaiseOnClientConnectedOrDisconnectedEvent;
                NetworkManager.Singleton.OnClientDisconnectCallback -= RaiseOnClientConnectedOrDisconnectedEvent;
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

        private void ApproveConnection(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = SceneManager.GetActiveScene().name == "MainMenu";
        }

        private void RaiseOnClientConnectedOrDisconnectedEvent(ulong clientId)
        {
            OnClientConnectedOrDisconnectedEvent?.Invoke();
            if (NetworkManager.Singleton.IsHost)
                Debug.Log($"New count of clients: {NetworkManager.Singleton.ConnectedClientsList.Count}");
        }
    }
}