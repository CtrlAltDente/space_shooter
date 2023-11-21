using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceShooter.Player
{
    public struct PlayerBodyData : INetworkSerializable
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

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Head);
            serializer.SerializeValue(ref LeftHand);
            serializer.SerializeValue(ref RightHand);
        }
    }
}