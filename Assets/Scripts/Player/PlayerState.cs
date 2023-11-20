using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : MonoBehaviour
    {
        public uint PlayerId = 0;

        [SerializeField]
        private PlayerSkin _skin;

        public void SetData(PlayerData playerData)
        {
            SetBodyData(playerData.PlayerBodyData);
        }

        private void SetBodyData(PlayerBodyData playerBodyData)
        {
            _skin.SetBodyData(playerBodyData);
        }
    }
}