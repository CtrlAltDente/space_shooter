using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceShooter.GameLogic
{
    public class SceneLoader : MonoBehaviour
    {
        [Inject]
        public ScreenFader Fader;

        private Coroutine _sceneLoadingCoroutine;

        public void LoadScene(string sceneName)
        {
            if (_sceneLoadingCoroutine == null)
            {
                Fader.FadeOut(() => _sceneLoadingCoroutine = StartCoroutine(StartLoadScene(sceneName)));
            }
        }

        private IEnumerator StartLoadScene(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            
            while (!asyncOperation.isDone)
            {
                if(asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}