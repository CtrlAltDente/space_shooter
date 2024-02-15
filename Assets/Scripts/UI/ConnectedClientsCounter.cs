using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class ConnectedClientsCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _connectedClientsLabel;

        private void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += ShowConnectedClients;
        }

        private void OnDestroy()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= ShowConnectedClients;
        }

        private void ShowConnectedClients(ulong count)
        {
            if (!NetworkManager.Singleton.IsHost)
                return;

            StartCoroutine(UpdateClientsCount());
        }

        private IEnumerator UpdateClientsCount()
        {
            yield return new WaitForSeconds(1f);
            _connectedClientsLabel.text = $"Clients in session: {NetworkManager.Singleton.ConnectedClients.Count}";
        }
    }
}