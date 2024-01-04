using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerBodyReferences : MonoBehaviour
    {
        public Transform Head;
        public Transform LeftHand;
        public Transform RightHand;

        public PlayerBodyData BodyData
        {
            get
            {
                return new PlayerBodyData(Head, LeftHand, RightHand);
            }
            set
            {
                SetDataForPart(Head, value.Head);
                SetDataForPart(LeftHand, value.LeftHand);
                SetDataForPart(RightHand, value.RightHand);
            }
        }

        private void SetDataForPart(Transform part, PlayerBodyPartInformation playerBodyPart)
        {
            part.position = playerBodyPart.Position;
            part.rotation = playerBodyPart.Rotation;
        }
    } 
}