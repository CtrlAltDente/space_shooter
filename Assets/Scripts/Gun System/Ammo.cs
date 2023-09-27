using SpaceShooter.Base;
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
        private float _damage = 20f;

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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.transform.GetComponent<IDamagable>() != null)
            {
                other.gameObject.GetComponent<IDamagable>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }


        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(_destroyTime);

            Destroy(gameObject);
        }
    }
}