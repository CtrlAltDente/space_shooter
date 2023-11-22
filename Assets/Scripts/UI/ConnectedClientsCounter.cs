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

        public void UpdateClientsCount()
        {
            if (NetworkManager.Singleton.IsHost)
                StartCoroutine(UpdateClientsCountWithDelay(1f));
        }

        private IEnumerator UpdateClientsCountWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _connectedClientsLabel.text = $"Clients in session: {NetworkManager.Singleton.ConnectedClientsList.Count}";
        }
    }
}