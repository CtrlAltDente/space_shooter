using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public struct PlayerBodyData
    {
        public PlayerBodyPartInformation Head;
        public PlayerBodyPartInformation LeftHand;
        public PlayerBodyPartInformation RightHand;

        public PlayerBodyData(Transform head, Transform leftHand, Transform rightHand)
        {
            Head = new PlayerBodyPartInformation(head);
            LeftHand = new PlayerBodyPartInformation(leftHand);
            RightHand = new PlayerBodyPartInformation(rightHand);
        }
    }
}