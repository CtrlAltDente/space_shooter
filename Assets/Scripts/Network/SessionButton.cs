using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Network
{
    public class SessionButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _idLabel;
        [SerializeField]
        private TextMeshProUGUI _ipLabel;

        [SerializeField]
        private Button _button;

        private void Start()
        {
            SubscribeOnEvents();
        }

        public void SetData(int id, string ip)
        {
            _idLabel.text = id.ToString();
            _ipLabel.text = ip;
        }

        private void SubscribeOnEvents()
        {
            _button.onClick.AddListener(ConnectToTheSession);
        }

        private void ConnectToTheSession()
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(_ipLabel.text, (ushort)LocalNetworkInfo.DefaultPort);
            NetworkManager.Singleton.StartClient();
        }
    }
}