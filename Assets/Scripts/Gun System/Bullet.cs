using SpaceShooter.Base;
using SpaceShooter.Enums;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private BulletOwnerType _type;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private float _movementSpeed = 10;

        [SerializeField]
        private float _damage = 20f;

        [SerializeField]
        private float _destroyTime = 30f;

        private void Start()
        {
            StartMoving();
            StartCoroutine(SelfDestroy());
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            
            if (NetworkManager.Singleton.IsHost)
            {
                if (collision.transform.GetComponent<IDamagable>() != null)
                {
                    collision.gameObject.GetComponent<IDamagable>().TakeDamage(_type, _damage);

                    Debug.Log("Damage");
                }
            }

            Destroy(gameObject);
        }

        public void SetBulletType(BulletOwnerType bulletType)
        {
            _type = bulletType;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        private void StartMoving()
        {
            _rigidbody.AddForce(transform.forward * _movementSpeed, ForceMode.Impulse);
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
        }
    }
}