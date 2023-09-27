using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.GunSystem
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField]
        private float _movementSpeed = 50f;

        [SerializeField]
        private float _destroyTime = 30f;

        private void Start()
        {
            StartCoroutine(SelfDestroy());
        }

        private void Update()
        {
            transform.position += transform.forward * _movementSpeed * Time.deltaTime;
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(gameObject);
        }
    }
}