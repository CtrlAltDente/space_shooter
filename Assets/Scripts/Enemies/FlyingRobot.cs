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
        private List<Transform> _players = new List<Transform>();

        private void Start()
        {
            if (IsHost || !NetworkManager.Singleton)
            {
                StartCoroutine(MainLogic());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("FINDED PLAYER");
                if (!_players.Contains(other.transform))
                {
                    _players.Add(other.transform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (_players.Contains(other.transform))
                {
                    _players.Remove(other.transform);
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

        public void Destroy()
        {
            if(IsHost)
            {
                NetworkObject.Despawn(true);
            }
        }

        private IEnumerator MainLogic()
        {
            while (_healthSystem.Health > 0)
            {
                Fly();
                AttackNearPlayer();

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

        private void AttackNearPlayer()
        {
            if (_players.Count > 0)
            {
                Vector3 direction = _players[0].transform.position - transform.position;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 30 * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction)) < 20f)
                {
                    Attack();
                }
            }
        }
    }
}