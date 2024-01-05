using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class GameCloser : MonoBehaviour
    {
        public void ExitGame()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
#elif !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }
}