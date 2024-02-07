using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasToCameraFader : MonoBehaviour
    {
        [SerializeField]
        private float _viewAngle = 25f;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            SetCanvasAlpha();
        }

        private void SetCanvasAlpha()
        {
            Vector3 directionToCamera = Camera.main.transform.position - transform.position;

            float canvasToCameraAngle = Vector3.Angle(transform.forward, directionToCamera);

            float visionValue = Mathf.Clamp01(_viewAngle / canvasToCameraAngle);

            _canvasGroup.alpha = visionValue;
        }
    }
}