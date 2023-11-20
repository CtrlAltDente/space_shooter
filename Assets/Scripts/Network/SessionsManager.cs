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

namespace SpaceShooter.Network
{
    public class SessionsManager : MonoBehaviour
    {
        private UdpClient _udpClient;

        [SerializeField]
        private SessionsList _sessionsList;

        [SerializeField]
        private TextMeshProUGUI _sessionId;
        [SerializeField]
        private TextMeshProUGUI _sessionIp;

        [SerializeField]
        private SessionInfo _createdSessionInfo;

        private void OnDestroy()
        {
            CloseUdpClient();
        }

        public void StartSession()
        {
            string localIpAddress = LocalNetworkInfo.GetLocalIPAddress();
            _createdSessionInfo = new SessionInfo(UnityEngine.Random.Range(10001, 19999), localIpAddress);

            _sessionId.text = $"Session Id: {_createdSessionInfo.SessionId}";
            _sessionIp.text = $"Session Ip: {_createdSessionInfo.SessionIp}";

            StartUdpClient(LocalNetworkInfo.DefaultPort + 1);
            StartCoroutine(StartSessionSharing());
        }

        public void StartSessionSearch()
        {
            StartUdpClient(LocalNetworkInfo.DefaultPort);
            StartCoroutine(StartSessionListening());
        }

        private void StartUdpClient(int port)
        {
            _udpClient = new UdpClient(port);
            _udpClient.JoinMulticastGroup(LocalNetworkInfo.DefaultMulticastIpAddress);
        }

        private void CloseUdpClient()
        {
            if (_udpClient != null)
            {
                SendSession(new SessionInfo(_createdSessionInfo.SessionId, "0.0.0.0"));
                _udpClient.DropMulticastGroup(LocalNetworkInfo.DefaultMulticastIpAddress);
                _udpClient.Close();
                _udpClient = null;
                Debug.Log("UDP CLIENT DESTROYED");
            }
        }

        private IEnumerator StartSessionSharing()
        {
            while (_udpClient != null)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log("Send");
                SendSession(_createdSessionInfo);
            }
        }

        private IEnumerator StartSessionListening()
        {
            while (_udpClient != null)
            {
                yield return new WaitForSeconds(1f);
                ReceiveSession();
            }
        }

        private async void SendSession(SessionInfo sessionInfo)
        {
            if (_udpClient != null)
            {
                byte[] sessionInfoData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(sessionInfo));
                await _udpClient.SendAsync(sessionInfoData, sessionInfoData.Length, new IPEndPoint(LocalNetworkInfo.DefaultMulticastIpAddress, LocalNetworkInfo.DefaultPort));
            }
        }

        private async void ReceiveSession()
        {
            var receiveResult = await _udpClient.ReceiveAsync();
            Debug.Log("Received data");
            _sessionsList.ProceedReceivedBytes(receiveResult.Buffer);
        }
    }
}