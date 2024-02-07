using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField]
        private PlayerState _playerState;

        [SerializeField]
        private GameObject _playerTopInformation;
        [SerializeField]
        private GameObject _handInformation;

        private void Start()
        {
            SetActiveInformationVisuals();
        }

        private void SetActiveInformationVisuals()
        {
            _playerTopInformation.SetActive(!_playerState.IsOwner);
            _handInformation.SetActive(_playerState.IsOwner);
        }
    }
}