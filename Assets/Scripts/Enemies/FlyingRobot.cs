using SpaceShooter.Base;
using SpaceShooter.Guns;
using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Enemies
{
    public class FlyingRobot : NetworkBehaviour, IEnemy, IDamagable
    {
        [SerializeField]
        private HealthSystem _healthSystem;

        [SerializeField]
        private Gun _gun;

        [SerializeField]
        private float _flyingHeight;

        [SerializeField]
        private float _timeAttackPause = 5f;

        [SerializeField]
        private List<PlayerState> _playerStates = new List<PlayerState>();

        private void Start()
        {
            if (IsHost || !NetworkManager.Singleton)
            {
                StartCoroutine(MainLogic());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerState>())
            {
                Debug.Log("FINDED PLAYER");
                if (!_playerStates.Contains(other.gameObject.GetComponent<PlayerState>()))
                {
                    _playerStates.Add(other.GetComponent<PlayerState>());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerState>())
            {
                if (_playerStates.Contains(other.gameObject.GetComponent<PlayerState>()))
                {
                    _playerStates.Remove(other.GetComponent<PlayerState>());
                }
            }
        }

        public void Attack()
        {
            _gun.Shoot();
        }

        public void TakeDamage(BulletType bulletType, float damage)
        {
            if (bulletType == BulletType.PlayerBullet)
            {
                _healthSystem.TakeDamage(bulletType, damage);
            }
        }

        private IEnumerator MainLogic()
        {
            while (_healthSystem.Health > 0)
            {
                Fly();

                yield return null;
            }

            if (IsHost)
            {
                NetworkObject.Despawn(true);
            }
        }

        private void Fly()
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag != "Enemy")
                {
                    Vector3 direction = (hit.point - transform.position);
                    transform.position += (direction + Vector3.up * _flyingHeight) * Time.deltaTime;
                }
            }
        }
    }
}