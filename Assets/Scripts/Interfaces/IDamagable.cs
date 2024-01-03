using SpaceShooter.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(BulletOwnerType bulletType, float damage);
}
