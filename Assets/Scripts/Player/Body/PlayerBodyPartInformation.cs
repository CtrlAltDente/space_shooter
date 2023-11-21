using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceShooter.Player
{
    public struct PlayerBodyPartInformation : INetworkSerializable
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public PlayerBodyPartInformation(Transform transform)
        {
            Position = transform.position;
            Rotation = transform.rotation;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Position);
            serializer.SerializeValue(ref Rotation);
        }
    }
}