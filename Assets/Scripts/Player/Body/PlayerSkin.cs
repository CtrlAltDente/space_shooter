using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField]
        private PlayerBodyReferences _playerBodyReferences;

        public void SetBodyData(PlayerBodyData playerSkinData)
        {
            SetDataForPart(playerSkinData.Head, _playerBodyReferences.Head);
            SetDataForPart(playerSkinData.LeftHand, _playerBodyReferences.LeftHand);
            SetDataForPart(playerSkinData.RightHand, _playerBodyReferences.RightHand);
        }

        private void SetDataForPart(PlayerBodyPartInformation playerBodyPart, Transform part)
        {
            part.position = playerBodyPart.Position;
            part.rotation = playerBodyPart.Rotation;
        }
    }
}