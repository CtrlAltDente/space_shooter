using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    public static class GameData
    {
        #region Values 

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