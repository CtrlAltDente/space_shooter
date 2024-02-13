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
    public class SkinsInitializer : MonoBehaviour, IInitializer
    {
        [SerializeField]
        private SkinsContainer _skinsContainer;

        [SerializeField]
        private SkinPart _head;
        [SerializeField]
        private SkinPart _leftHand;
        [SerializeField]
        private SkinPart _rightHand;

        public void Initialize(UserConfig userConfig)
        {
            InitializeSkin(userConfig.SkinIndex);
        }

        private void InitializeSkin(int skinIndex)
        {
            InstantiateSkinPartModel(_head, _skinsContainer.Items[skinIndex].Head);
            InstantiateSkinPartModel(_leftHand, _skinsContainer.Items[skinIndex].LeftHand);
            InstantiateSkinPartModel(_rightHand, _skinsContainer.Items[skinIndex].RightHand);
        }

        private void InstantiateSkinPartModel(SkinPart playerPart, SkinPart containerPart)
        {
            playerPart.MeshFilter.sharedMesh = containerPart.MeshFilter.sharedMesh;
            playerPart.MeshRenderer.sharedMaterials= containerPart.MeshRenderer.sharedMaterials;
            playerPart.MeshCollider.sharedMesh = playerPart.MeshFilter.sharedMesh;
        }
    }
}