using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField]
        private PlayerBodyReference _playerBodyReference;

        public void SetBodyData(PlayerBodyData playerSkinData)
        {
            SetDataForPart(playerSkinData.Head, _playerBodyReference.Head);
            SetDataForPart(playerSkinData.LeftHand, _playerBodyReference.LeftHand);
            SetDataForPart(playerSkinData.RightHand, _playerBodyReference.RightHand);
        }

        private void SetDataForPart(PlayerBodyPartInformation playerBodyPart, Transform part)
        {
            part.position = playerBodyPart.Position;
            part.rotation = playerBodyPart.Rotation;
        }
    }
}