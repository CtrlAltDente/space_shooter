using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IInitializer
    {
        public void Initialize(UserConfig userConfig);
    }
}