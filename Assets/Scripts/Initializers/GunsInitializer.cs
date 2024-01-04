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
            _gunsPresets[gunIndex].SetActiveGuns(true);
        }
    }
}