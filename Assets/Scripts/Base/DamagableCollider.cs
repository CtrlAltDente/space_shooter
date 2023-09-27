using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Base
{
    public class DamagableCollider : MonoBehaviour, IDamagable
    {
        public UnityEvent<float> OnDamageTaked;

        public void TakeDamage(float damage)
        {
            OnDamageTaked?.Invoke(damage);
        }
    }
}