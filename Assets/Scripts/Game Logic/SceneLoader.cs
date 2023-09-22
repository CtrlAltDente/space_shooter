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
        [Inject]
        public Loadbar Loader;

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
            asyncOperation.allowSceneActivation = false;

            Loader.gameObject.SetActive(true);

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    Loader.gameObject.SetActive(false);

                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}