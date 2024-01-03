using SpaceShooter.Enums;
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

        private void Start()
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                this.enabled = false;
            }
        }

        public void TakeDamage(BulletOwnerType bulletType, float damage)
        {
            OnDamageTaked?.Invoke(bulletType, damage);
        }
    }
}