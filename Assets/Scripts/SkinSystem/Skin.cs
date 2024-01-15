using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Skins
{
    public class Skin : MonoBehaviour
    {
        [SerializeField]
        private SkinPart _head;
        [SerializeField]
        private SkinPart _leftHand;
        [SerializeField]
        private SkinPart _rightHand;

        public void SetPlayerData(PlayerData playerData)
        {
            SetSkinRefences(playerData.PlayerBodyData);
        }

        private void SetSkinRefences(PlayerBodyData playerBodyData)
        {
            SetTransformValues(_head, playerBodyData.Head);
            SetTransformValues(_leftHand, playerBodyData.LeftHand);
            SetTransformValues(_rightHand, playerBodyData.RightHand);
        }

        private void SetTransformValues(SkinPart skinPart, PlayerBodyPartInformation bodyPartInformation)
        {
            skinPart.transform.position = bodyPartInformation.Position;
            skinPart.transform.rotation = bodyPartInformation.Rotation;
        }
    }
}