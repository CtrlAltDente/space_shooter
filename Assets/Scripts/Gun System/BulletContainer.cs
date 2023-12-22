using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Guns
{
    [Serializable]
    public class BulletContainer
    {
        public Bullet Bullet;
        public BulletType BulletType;
    }
}