using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.GameLogic.Session
{
    public class GameSessionInformationDisplayer : MonoBehaviour
    {
        [SerializeField]
        private GameSession _gameSession;

        [SerializeField]
        private TextMeshProUGUI _informationText;

        private void Update()
        {
            DisplayInformation();
        }

        private void DisplayInformation()
        {
            _informationText.text = _gameSession.SessionInformation.Value.ToString();
        }
    }
}