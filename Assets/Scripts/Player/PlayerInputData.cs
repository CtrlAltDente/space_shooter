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

        public bool LeftStickPressed;
        public bool RightStickPressed;

        public PlayerInputData(bool leftTriggerPressed, bool rightTriggerPressed, bool leftStickPressed, bool rightStickPressed)
        {
            LeftTriggerPressed = leftTriggerPressed;
            RightTriggerPressed = rightTriggerPressed;
            LeftStickPressed = leftStickPressed;
            RightStickPressed = rightStickPressed;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref LeftTriggerPressed);
            serializer.SerializeValue(ref RightTriggerPressed);
            serializer.SerializeValue(ref LeftStickPressed);
            serializer.SerializeValue(ref RightStickPressed);
        }
    }
}