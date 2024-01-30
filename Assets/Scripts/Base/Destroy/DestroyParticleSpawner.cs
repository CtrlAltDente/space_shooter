using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class DestroyParticleSpawner : NetworkBehaviour
    {
        [SerializeField]
        private DestroyParticle _destroyParticle;

        public override void OnDestroy()
        {
            if (IsHost)
            {
                DestroyParticle afterParticle = Instantiate(_destroyParticle, transform.position, transform.rotation, null);
                afterParticle.NetworkObject.Spawn();
            }

            base.OnDestroy();
        }
    }
}