using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Extensions
{
    public static class MonoBehaviourExtension
    {
        public static void KillCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine)
        {
            if(coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}
