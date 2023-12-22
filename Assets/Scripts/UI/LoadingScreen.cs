using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] 
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Loadbar _loadbar;

        private bool _inProgress = false;

        public float LoadProgression
        {
            set
            {
                _loadbar.SetLoadProgress(value);
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            FadeIn();
        }

        public void FadeIn(TweenCallback callback = null)
        {
            if (_inProgress)
                return;

            _loadbar.gameObject.SetActive(true);
            _inProgress = true;
            Fade(0f, callback);
        }

        public void FadeOut(TweenCallback callback = null)
        {
            if (_inProgress)
                return;

            _loadbar.gameObject.SetActive(true);
            _inProgress = true;
            Fade(1f, callback);
        }

        private void Fade(float endvalue, TweenCallback callback = null)
        {
            _canvasGroup.DOFade(endvalue, 1f).OnComplete(() =>
            {
                callback?.Invoke();
                _inProgress = false;
                _loadbar.gameObject.SetActive(false);
            });
        }
    }
}