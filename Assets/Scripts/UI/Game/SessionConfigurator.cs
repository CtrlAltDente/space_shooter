using SpaceShooter.Data;
using SpaceShooter.Enums;
using SpaceShooter.GameLogic.Session;
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
        private TMP_Dropdown _sessionType;

        [SerializeField]
        private TextMeshProUGUI _sessionTypeValueText;
        
        [SerializeField]
        private TextMeshProUGUI _sessionTypeValueDescription;

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
            Debug.Log("Session type: " + GameData.SessionType);
            _sessionType.value = type;
            SetDescription();
        }

        private void SetSessionTypeValue(int value)
        {
            GameData.SessionTypeValue = Mathf.Clamp(value, 5, 50);

            Debug.Log("Type value:" + GameData.SessionTypeValue);
            _sessionTypeValueText.text = GameData.SessionTypeValue.ToString();
        }

        private void SetDescription()
        {
            switch (GameData.SessionType)
            {
                case SessionType.EnemyKills:
                    {
                        _sessionTypeValueDescription.text = "Select how enemies you want to destroy";
                        break;
                    }
                case SessionType.Time:
                    {
                        _sessionTypeValueDescription.text = "Select how much minutes you will fight";
                        break;
                    }
            }

        }
    }
}