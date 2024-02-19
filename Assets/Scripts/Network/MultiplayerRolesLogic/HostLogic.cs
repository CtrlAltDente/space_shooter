using SpaceShooter.Network.Sessions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Network.Roles
{
    public class HostLogic : MonoBehaviour
    {
        public UnityEvent OnLocalHostSelected;
        public UnityEvent OnInternetHostSelected;

        [SerializeField]
        private SessionsSharer _sessionSharer;

        public void SelectLocalHost()
        {
            try
            {
                string localIpAddress = LocalNetworkInfo.GetLocalIPAddress();
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(localIpAddress, (ushort)LocalNetworkInfo.DefaultPort);
                NetworkManager.Singleton.StartHost();
                NetworkManager.Singleton.ConnectionApprovalCallback += ApproveConnection;
                _sessionSharer.StartSession();
                OnLocalHostSelected?.Invoke();
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void SelectInternetHost()
        {

        }

        private void ApproveConnection(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = SceneManager.GetActiveScene().name == "MainMenu";
        }

    }
}