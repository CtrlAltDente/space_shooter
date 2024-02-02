using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class DestroyParticleSpawner : MonoBehaviour
    {
        [SerializeField]
        private DestroyParticle _destroyParticle;

        public void OnDestroy()
        {
            DestroyParticle afterParticle = Instantiate(_destroyParticle, transform.position, transform.rotation, null);
        }
    }
}