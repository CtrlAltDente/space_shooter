using SpaceShooter.Base;
using SpaceShooter.Enums;
using SpaceShooter.Guns;
using SpaceShooter.Interfaces;
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
        private Transform[] _gunObjects;

        [SerializeField]
        private float _flyingHeight;
        [SerializeField]
        private float _rotationSpeed = 20f;
        [SerializeField]
        private float _aimShootRange = 20f;

        [SerializeField]
        private List<Transform> _players = new List<Transform>();

        public HealthSystem HealthSystem => _healthSystem;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);

            if (IsHost)
            {
                StartCoroutine(MainLogic());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PlayerHead")
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
            if (other.gameObject.tag == "PlayerHead")
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

        public void TakeDamage(BulletOwnerType bulletType, float damage)
        {
            if (bulletType == BulletOwnerType.PlayerBullet)
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
            while (_healthSystem.CurrentHealth > 0)
            {
                Fly();
                AttackNearPlayer();

                yield return null;
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);
                ConfigureGunPositions(direction);
            }
        }

        private void ConfigureGunPositions(Vector3 targetDirection)
        {
            foreach(Transform gunPosition in _gunObjects)
            {
                RotateGunPositions(gunPosition);

                if (Quaternion.Angle(gunPosition.rotation, Quaternion.LookRotation(targetDirection)) < _aimShootRange)
                {
                    Attack();
                }
            }
        }

        private void RotateGunPositions(Transform positionTransform)
        {
            Quaternion newTransformRotation = Quaternion.RotateTowards(positionTransform.rotation, Quaternion.LookRotation(_players[0].transform.position - positionTransform.position), _rotationSpeed * Time.deltaTime);
            positionTransform.rotation = newTransformRotation; 
        }
    }
}