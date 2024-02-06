using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class HUDHealthIndicator : HealthIndicator
    {
        [SerializeField]
        private LocalUser _localUser;

        private new void Update()
        {
            GetPlayerState();
            base.Update();
        }

        private void GetPlayerState()
        {
            if (_playerState == null)
            {
                _playerState = _localUser.PlayerState;
            }
        }
    }
}