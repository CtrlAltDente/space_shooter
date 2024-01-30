using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class DestroyParticle : NetworkBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private float _destroyTime;

        private void Start()
        {
            if (IsHost)
            {
                StartCoroutine(SelfDestroy());
            }

            _particleSystem.Play();
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(_destroyTime);
            NetworkObject.Despawn(true);
        }
    }
}