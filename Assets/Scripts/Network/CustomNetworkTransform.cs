using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace SpaceShooter.Network
{
    public class CustomNetworkTransform : NetworkBehaviour
    {
        [SerializeField]
        private NetworkVariable<CoreTransformValue> Transform = new NetworkVariable<CoreTransformValue>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        private void Update()
        {
            WriteValue();
            SetValue();
        }

        private void WriteValue()
        {
            if (IsOwner)
            {
                Transform.Value = new CoreTransformValue(transform.position, transform.rotation);
            }
        }

        private void SetValue()
        {
            if (!IsOwner)
            {
                transform.position = Transform.Value.Position;
                transform.rotation = Transform.Value.Rotation;
            }
        }
    }

    [Serializable]
    public struct CoreTransformValue : INetworkSerializable
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public CoreTransformValue(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Position);
            serializer.SerializeValue(ref Rotation);
        }
    }
}