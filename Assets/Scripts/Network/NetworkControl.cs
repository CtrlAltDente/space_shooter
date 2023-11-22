using SpaceShooter.GameLogic;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using System.Threading.Tasks;

namespace SpaceShooter.Network
{
    public class NetworkControl : MonoBehaviour
    {
        public UnityEvent OnHostSelected;
        public UnityEvent OnClientSelected;

        public UnityEvent<string> OnRelayHostSelected;
        public UnityEvent OnRelayClientSelected;

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

        public async void SelectRelayHost()
        {
            Task<string> task = StartHostWithRelay();
            await task;
            if (task.Result != null)
            {
                Debug.Log($"Join code: {task.Result}");
                OnRelayHostSelected?.Invoke(task.Result);
            }
            else
            {
                Debug.Log("Failed to start host on relay!");
            }
        }

        public async void SelectRelayClient(string joinCode)
        {
            Task<bool> task = StartClientWithRelay(joinCode);
            await task;
            if (task.Result)
            {
                OnRelayClientSelected?.Invoke();
            }
            else
            {
                Debug.Log("Cannot start client on this join code!");
            }
        }

        public void SelectHost()
        {
            string localIpAddress = LocalNetworkInfo.GetLocalIPAddress();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(localIpAddress, (ushort)LocalNetworkInfo.DefaultPort);
            NetworkManager.Singleton.StartHost();
            OnHostSelected?.Invoke();
        }

        public void SelectClient()
        {
            OnClientSelected?.Invoke();
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

        public async Task<string> StartHostWithRelay(int maxConnections = 5)
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
            var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            return NetworkManager.Singleton.StartHost() ? joinCode : null;
        }

        public async Task<bool> StartClientWithRelay(string joinCode)
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
            return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
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