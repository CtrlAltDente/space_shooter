using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceShooter.Player
{
    public struct PlayerInputData : INetworkSerializable
    {
        public bool LeftTriggerPressed;
        public bool RightTriggerPressed;

        public PlayerInputData(bool leftTriggerPressed, bool rightTriggerPressed)
        {
            LeftTriggerPressed = leftTriggerPressed;
            RightTriggerPressed = rightTriggerPressed;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref LeftTriggerPressed);
            serializer.SerializeValue(ref RightTriggerPressed);
        }
    }
}