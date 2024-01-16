using SpaceShooter.Player;
using SpaceShooter.ScriptableObjects;
using SpaceShooter.Skins;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class SkinsInitializer : MonoBehaviour
    {
        [SerializeField]
        private SkinsContainer _skinsContainer;

        [SerializeField]
        private SkinPart _head;
        [SerializeField]
        private SkinPart _leftHand;
        [SerializeField]
        private SkinPart _rightHand;

        public void InitializeSkin(int skinIndex)
        {
            InstantiateSkinPartModel(_head, _skinsContainer.Items[skinIndex].Head);
            InstantiateSkinPartModel(_leftHand, _skinsContainer.Items[skinIndex].LeftHand);
            InstantiateSkinPartModel(_rightHand, _skinsContainer.Items[skinIndex].RightHand);
        }

        private void InstantiateSkinPartModel(SkinPart playerPart, SkinPart containerPart)
        {
            RemoveSpawnedSkin(playerPart);
            playerPart.SkinModel = Instantiate(containerPart.SkinModel, playerPart.transform.position, Quaternion.identity, playerPart.transform);
        }

        private void RemoveSpawnedSkin(SkinPart playerPart)
        {
            if(playerPart.SkinModel)
            {
                Destroy(playerPart.SkinModel);
                playerPart.SkinModel = null;
            }
        }
    }
}