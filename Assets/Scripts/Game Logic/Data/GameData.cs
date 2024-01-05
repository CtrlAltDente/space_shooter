using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    public static class GameData
    {
        public static int PlayerConfigurationIndex
        {
            get
            {
                if (PlayerPrefs.HasKey(GameDataKeys.PLAYER_CONFIGURATION_INDEX))
                {
                    return PlayerPrefs.GetInt(GameDataKeys.PLAYER_CONFIGURATION_INDEX);
                }
                else
                {
                    PlayerPrefs.SetInt(GameDataKeys.PLAYER_CONFIGURATION_INDEX, 0);
                    return 0;
                }
            }
            set
            {
                PlayerPrefs.SetInt(GameDataKeys.PLAYER_CONFIGURATION_INDEX, value);
            }
        }

        public static string PlayerName
        {
            get
            {
                if (PlayerPrefs.HasKey(GameDataKeys.PLAYER_NAME))
                {
                    return PlayerPrefs.GetString(GameDataKeys.PLAYER_NAME);
                }
                else
                {
                    PlayerPrefs.SetString(GameDataKeys.PLAYER_NAME, "Player");
                    return PlayerPrefs.GetString(GameDataKeys.PLAYER_NAME);
                }
            }
            set
            {
                if (value != null)
                {
                    PlayerPrefs.SetString(GameDataKeys.PLAYER_NAME, value);
                }
                else
                {
                    PlayerPrefs.SetString(GameDataKeys.PLAYER_NAME, string.Empty);
                }
            }
        }
    }
}