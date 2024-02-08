using SpaceShooter.LifeSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Container_LifeSupportSystems", menuName = "Scriptable Objects/Containers/Life Support Systems Container", order = 2)]
    public class LifeSupportSystemsContainer : Container<LifeSupportSystem>
    {

    }
}