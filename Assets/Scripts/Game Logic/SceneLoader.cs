using DG.Tweening;
using SpaceShooter.UI;
using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceShooter.GameLogic
{
    public class SceneLoader : MonoBehaviour
    {
        public LoadingScreen LoadingScreen => _loadingScreen;

        [Inject]
        private LoadingScreen _loadingScreen;

        private Coroutine _sceneLoadingCoroutine;

        public void LoadNetworkScene(string sceneName, Action ActionBeforeSceneChanging = null)
        {
            if (_sceneLoadingCoroutine == null)
            {
                _loadingScreen.LoadProgression = 1f;

                _loadingScreen.FadeOut(() =>
                {
                    if(NetworkManager.Singleton.IsHost)
                    {
                        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                    }
                });
            }
        }

        public void LoadScene(string sceneName, Action ActionBeforeSceneChanging = null)
        {
            if (_sceneLoadingCoroutine == null)
            {
                _loadingScreen.FadeOut(() =>
                {
                    ActionBeforeSceneChanging?.Invoke();
                    _sceneLoadingCoroutine = StartCoroutine(StartLoadScene(sceneName));
                });
            }
        }

        private IEnumerator StartLoadScene(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                _loadingScreen.LoadProgression = asyncOperation.progress;

                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}