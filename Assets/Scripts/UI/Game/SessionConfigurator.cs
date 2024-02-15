using SpaceShooter.Enums;
using SpaceShooter.GameLogic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.UI.Game
{
    public class SessionConfigurator : MonoBehaviour
    {
        public SessionConfig SessionConfig => GameData.SessionConfig;

        [SerializeField]
        private TextMeshProUGUI _sessionTypeValueText;

        private void Start()
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            SetSessionType((int)GameData.SessionType);
            SetSessionTypeValue(GameData.SessionTypeValue);
        }

        public void IncreaseValue()
        {
            SetSessionTypeValue(GameData.SessionTypeValue + 1);
        }

        public void DecreaseValue()
        {
            SetSessionTypeValue(GameData.SessionTypeValue - 1);
        }

        public void SetSessionType(int type)
        {
            GameData.SessionType = (SessionType)type;
        }

        private void SetSessionTypeValue(int value)
        {
            GameData.SessionTypeValue = Mathf.Clamp(value, 0, 50);
            _sessionTypeValueText.text = GameData.SessionTypeValue.ToString();
        }
    }
}