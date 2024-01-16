using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Container_Guns", menuName = "Scriptable Objects/Containers/Guns Container", order = 1)]
    public class GunsContainer : Container<GunPreset>
    {

    }
}