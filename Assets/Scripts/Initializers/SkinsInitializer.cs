using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class SkinsInitializer : MonoBehaviour
    {
        [SerializeField]
        private SkinPreset[] _skinPresets;

        public void InitializeSkin(int skinPresetIndex)
        {
            ResetSkins();

            _skinPresets[skinPresetIndex].SetActiveSkin(true);
        }

        private void ResetSkins()
        {
            foreach(SkinPreset skinPreset in _skinPresets)
            {
                skinPreset.SetActiveSkin(false);
            }
        }
    }
}