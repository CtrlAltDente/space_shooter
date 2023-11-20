using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : NetworkBehaviour
    {
        public ulong PlayerId = 0;

        [SerializeField]
        private PlayerSkin _skin;

        private void Start()
        {
            InitializeInNetwork();
        }

        public void SetData(PlayerData playerData)
        {
            SetBodyData(playerData.PlayerBodyData);
        }

        private void SetBodyData(PlayerBodyData playerBodyData)
        {
            _skin.SetBodyData(playerBodyData);
        }

        private void InitializeInNetwork()
        {
            if (!NetworkManager.Singleton)
                return;

            if(NetworkManager.Singleton.IsHost)
            {
                GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}