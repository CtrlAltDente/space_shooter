using SpaceShooter.Enums;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Base
{
    public class DamagableCollider : MonoBehaviour, IDamagable
    {
        public UnityEvent<BulletOwnerType, float> OnDamageTaked;

        public void TakeDamage(BulletOwnerType bulletType, float damage)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                OnDamageTaked?.Invoke(bulletType, damage);
            }
        }
    }
}