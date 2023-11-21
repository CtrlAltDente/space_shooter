using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

namespace SpaceShooter.Network
{
    public class SessionsSharer : MonoBehaviour
    {
        private UdpClient _udpClient;

        [SerializeField]
        private TextMeshProUGUI _sessionId;
        [SerializeField]
        private TextMeshProUGUI _sessionIp;

        [SerializeField]
        private SessionInfo _createdSessionInfo;

        private Coroutine _networkCoroutine;

        public void StartSession()
        {
            string localIpAddress = LocalNetworkInfo.GetLocalIPAddress();
            _createdSessionInfo = new SessionInfo(Random.Range(10001, 19999), localIpAddress);

            _sessionId.text = $"Session Id: {_createdSessionInfo.SessionId}";
            _sessionIp.text = $"Session Ip: {_createdSessionInfo.SessionIp}";

            StartUdpClient(LocalNetworkInfo.DefaultPort + 1);
            _networkCoroutine = StartCoroutine(StartSessionSharing());
        }

        public void CloseUdpClient()
        {
            if (_udpClient != null)
            {
                StopCoroutine(_networkCoroutine);
                SendSession(new SessionInfo(_createdSessionInfo.SessionId, "0.0.0.0"), true);
                Debug.Log("UDP CLIENT DESTROYED");
            }
        }

        private void StartUdpClient(int port)
        {
            _udpClient = new UdpClient(port);
            _udpClient.JoinMulticastGroup(LocalNetworkInfo.DefaultMulticastIpAddress);
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

        private async void SendSession(SessionInfo sessionInfo, bool destroyAtEnd = false)
        {
            if (_udpClient != null)
            {
                byte[] sessionInfoData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(sessionInfo));
                await _udpClient.SendAsync(sessionInfoData, sessionInfoData.Length, new IPEndPoint(LocalNetworkInfo.DefaultMulticastIpAddress, LocalNetworkInfo.DefaultPort));

                if (destroyAtEnd)
                {
                    _udpClient.Close();
                    _udpClient = null;
                }
            }
        }
    }
}