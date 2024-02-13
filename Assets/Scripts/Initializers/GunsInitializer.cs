using SpaceShooter.Guns;
using SpaceShooter.Interfaces;
using SpaceShooter.Player;
using SpaceShooter.ScriptableObjects;
using SpaceShooter.Skins;
using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class GunsInitializer : NetworkBehaviour, IInitializer
    {
        [SerializeField]
        private GunsContainer _gunsContainer;

        [SerializeField]
        private ShootSystem _shootSystem;

        [SerializeField]
        private PlayerHand _leftHand;
        [SerializeField]
        private PlayerHand _rightHand;

        public void Initialize(UserConfig userConfig)
        {
            InitializeGun(userConfig.GunIndex);
        }

        private void InitializeGun(int gunIndex)
        {
            GunPreset gunPreset = _gunsContainer.Items[gunIndex];

            RemoveInitializedGun(_leftHand);
            RemoveInitializedGun(_rightHand);

            if (!gunPreset.IsTwoHanded)
            {
                InitializeOneHandedGuns(gunPreset);
            }
            else
            {
                InitializeTwoHandedGun(gunPreset);
            }
        }

        private void InitializeOneHandedGuns(GunPreset gunPreset)
        {
            Gun gun = gunPreset.Gun;

            InitializeGun(gun, _leftHand);
            InitializeGun(gun, _rightHand);
        }

        private void InitializeTwoHandedGun(GunPreset gunPreset)
        {
            Gun gun = gunPreset.Gun;

            InitializeGun(gun, _rightHand, _leftHand);
        }

        private void InitializeGun(Gun gun, PlayerHand coreHand, PlayerHand additionalHand = null)
        {
            Gun spawnedGun = Instantiate(gun, coreHand.transform.position, coreHand.transform.rotation, coreHand.transform);
            spawnedGun.SetShootSystem(_shootSystem);
            spawnedGun.SetHands(coreHand, additionalHand);
        }

        private void RemoveInitializedGun(PlayerHand coreHand)
        {
            coreHand.ClearInteractableItem();
        }
    }
}