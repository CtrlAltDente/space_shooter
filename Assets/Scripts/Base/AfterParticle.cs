using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class AfterParticle : NetworkBehaviour
    {
        [SerializeField]
        private float _destroyTime;

        private void Start()
        {
            if (IsHost)
            {
                StartCoroutine(SelfDestroy());
            }

            GetComponent<ParticleSystem>().Play();
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(_destroyTime);
            NetworkObject.Despawn(true);
        }
    }
}