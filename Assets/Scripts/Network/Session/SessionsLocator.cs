using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Network.Sessions
{
    public class SessionsLocator : MonoBehaviour
    {
        [SerializeField]
        private SessionsList _sessionsList;

        private UdpClient _udpClient;

        private Coroutine _networkCoroutine;

        public void StartSessionSearch()
        {
            StartUdpClient(LocalNetworkInfo.DefaultPort);
            _networkCoroutine = StartCoroutine(StartSessionListening());
        }

        public void CloseUdpClient()
        {
            if (_udpClient != null)
            {
                StopCoroutine(_networkCoroutine);
                _udpClient.Close();
                _udpClient = null;
                Debug.Log("UDP CLIENT DESTROYED");
            }
        }

        private void StartUdpClient(int port)
        {
            _udpClient = new UdpClient(port);
            _udpClient.JoinMulticastGroup(LocalNetworkInfo.DefaultMulticastIpAddress);
        }

        private IEnumerator StartSessionListening()
        {
            while (_udpClient != null)
            {
                yield return new WaitForSeconds(1f);
                ReceiveSession();
            }
        }

        private async void ReceiveSession()
        {
            if (_udpClient != null)
            {
                var receiveResult = await _udpClient.ReceiveAsync();
                Debug.Log("Received data");
                _sessionsList.ProceedReceivedBytes(receiveResult.Buffer);
            }
        }
    }
}