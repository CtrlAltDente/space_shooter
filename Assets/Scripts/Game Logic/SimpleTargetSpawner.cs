using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    public class SimpleTargetSpawner : MonoBehaviour
    {
        [SerializeField]
        [Range(2, 15)]
        private int _spawnHorizontalRadius;

        [SerializeField]
        private int _targetsAtStart = 10;

        [SerializeField]
        private DamagableCollider _simpleTargetPrefab;

        private void Start()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                for (int i = 0; i < _targetsAtStart; i++)
                {
                    SpawnPrefabAtRandomPositionInRadius();
                }
            }
        }

        private void SpawnPrefabAtRandomPositionInRadius()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                DamagableCollider simpleTarget = Instantiate(_simpleTargetPrefab, transform.position + new Vector3(Random.Range(-_spawnHorizontalRadius, _spawnHorizontalRadius), 0, Random.Range(-_spawnHorizontalRadius, _spawnHorizontalRadius)), Quaternion.identity, null);
                simpleTarget.OnDamageTaked.AddListener((bulletType, range) => { SpawnPrefabAtRandomPositionInRadius(); simpleTarget.GetComponent<NetworkObject>().Despawn(); Destroy(simpleTarget.gameObject); });
                simpleTarget.GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}