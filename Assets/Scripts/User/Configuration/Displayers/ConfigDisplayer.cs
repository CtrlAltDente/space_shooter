using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public abstract class ConfigDisplayer<T> : MonoBehaviour
    {
        [SerializeField]
        protected T _configChooser;

        public abstract void DisplayCurrentConfig(int configDataIndex);
    }
}