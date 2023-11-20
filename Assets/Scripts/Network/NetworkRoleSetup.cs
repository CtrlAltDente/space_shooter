using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Network
{
    public class NetworkRoleSetup : MonoBehaviour
    {
        public UnityEvent OnHostStarted;
        public UnityEvent OnClientStarted;

        public void StartHost()
        {
            string localIpAddress = LocalNetworkInfo.GetLocalIPAddress();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(localIpAddress, (ushort)LocalNetworkInfo.DefaultPort);
            OnHostStarted?.Invoke();
        }

        public void StartClient()
        {
            OnClientStarted?.Invoke();
        }
    }
}