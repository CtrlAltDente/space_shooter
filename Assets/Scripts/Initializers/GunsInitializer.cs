using SpaceShooter.Guns;
using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class GunsInitializer : NetworkBehaviour
    {
        [SerializeField]
        private GunsPreset[] _gunsPresets;

        public void InitializeGun(int gunIndex)
        {
            ResetGuns();

            _gunsPresets[gunIndex].SetActiveGuns(true);
        }

        private void ResetGuns()
        {
            foreach (GunsPreset gunPreset in _gunsPresets)
            {
                gunPreset.SetActiveGuns(false);
            }
        }
    }
}