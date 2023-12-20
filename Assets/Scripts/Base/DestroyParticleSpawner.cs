using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class DestroyParticleSpawner : NetworkBehaviour
    {
        [SerializeField]
        private AfterParticle _afterParticle;

        public override void OnDestroy()
        {
            if (IsHost)
            {
                AfterParticle afterParticle = Instantiate(_afterParticle, transform.position, transform.rotation, null);
                afterParticle.NetworkObject.Spawn();
            }

            base.OnDestroy();
        }
    }
}