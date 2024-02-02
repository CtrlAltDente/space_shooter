using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class DestroyParticle : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private float _destroyTime;

        private void Start()
        {
            StartCoroutine(SelfDestroy());
            _particleSystem.Play();
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
        }
    }
}