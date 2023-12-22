using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.ScriptableObjects
{
    public class Container<T> : ScriptableObject
    {
        public T[] Items;
    }
}