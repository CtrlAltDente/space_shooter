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
                if (PlayerPrefs.HasKey(GameDataKeys.PLAYER_CONFIGURATION_INDEX))
                {
                    PlayerPrefs.SetInt(GameDataKeys.PLAYER_CONFIGURATION_INDEX, value);
                }
            }
        }
    }
}