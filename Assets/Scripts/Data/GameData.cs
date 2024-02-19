using SpaceShooter.Enums;
using SpaceShooter.GameLogic.Session;
using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Data
{
    public static class GameData
    {
        #region Values 

        #region Session

        public static SessionConfig SessionConfig
        {
            get
            {
                return new SessionConfig(SessionType, SessionTypeValue);
            }
            set
            {
                SessionType = value.Type;
                SessionTypeValue = value.TypeValue;
            }
        }

        public static SessionType SessionType
        {
            get
            {
                return (SessionType)GetIntValue(GameDataKeys.SESSION_TYPE);
            }
            set
            {
                SetValue(GameDataKeys.SESSION_TYPE, (int)value);
            }
        }

        public static int SessionTypeValue
        {
            get
            {
                return GetIntValue(GameDataKeys.SESSION_TYPE_VALUE);
            }
            set
            {
                SetValue(GameDataKeys.SESSION_TYPE_VALUE, value);
            }
        }

        #endregion

        #region Player

        public static UserConfig GameUserConfig
        {
            get
            {
                return new UserConfig(PlayerName, SkinIndex, GunIndex, LifeSupportSystemIndex);
            }
        }

        public static int PlayerConfigurationIndex
        {
            get
            {
                return GetIntValue(GameDataKeys.PLAYER_CONFIGURATION_INDEX);
            }
            set
            {
                SetValue(GameDataKeys.PLAYER_CONFIGURATION_INDEX, value);
            }
        }

        public static int SkinIndex
        {
            get
            {
                return GetIntValue(GameDataKeys.SKIN_INDEX);
            }
            set
            {
                SetValue(GameDataKeys.SKIN_INDEX, value);
            }
        }

        public static int GunIndex
        {
            get
            {
                return GetIntValue(GameDataKeys.GUN_INDEX);
            }
            set
            {
                SetValue(GameDataKeys.GUN_INDEX, value);
            }
        }

        public static int LifeSupportSystemIndex
        {
            get
            {
                return GetIntValue(GameDataKeys.LIFE_SUPPORT_SYSTEM_INDEX);
            }
            set
            {
                SetValue(GameDataKeys.LIFE_SUPPORT_SYSTEM_INDEX, value);
            }
        }

        public static string PlayerName
        {
            get
            {
                return GetStringValue(GameDataKeys.PLAYER_NAME);
            }
            set
            {
                SetValue(GameDataKeys.PLAYER_NAME, value);
            }
        }

        #endregion

        #endregion

        #region Getting and setting values to PlayerPrefs logic

        private static int GetIntValue(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            else
            {
                PlayerPrefs.SetInt(key, 0);
                return 0;
            }
        }

        private static string GetStringValue(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }
            else
            {
                SetValue(key, "text");
                return PlayerPrefs.GetString(key);
            }
        }

        private static void SetValue(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        private static void SetValue(string key, string value)
        {
            if (value != null)
            {
                PlayerPrefs.SetString(key, value);
            }
            else
            {
                PlayerPrefs.SetString(key, string.Empty);
            }
        }

        #endregion
    }
}