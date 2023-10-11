using SpaceShooter.Interfaces;
using SpaceShooter.Spaceships;
using SpaceShooter.UI.Destroyer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.GameLogic
{
    public class WorldSpaceRadar : MonoBehaviour
    {
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private RadarTarget _radarTargetPrefab;
        [SerializeField]
        private List<RadarTarget> _radarTargets = new List<RadarTarget>();

        [SerializeField]
        private float _radarScaling = 500f;

        [SerializeField]
        private List<GameObject> _targetToAdd = new List<GameObject>(); //reworked in future

        private void Awake()
        {
            AddTargetsToRadar();
        }

        private void Update()
        {
            ShowDestroyersOnRadar();
        }

        public void AddTargetsToRadar()
        {
            foreach (GameObject targetGameObject in _targetToAdd)
            {
                if (targetGameObject.GetComponent<ITarget>() != null)
                {
                    RadarTarget newRadarTarget = Instantiate(_radarTargetPrefab, transform.position, Quaternion.identity, transform);
                    newRadarTarget.SetTarget(targetGameObject.GetComponent<ITarget>());
                    _radarTargets.Add(newRadarTarget);
                }
            }
        }

        private void ShowDestroyersOnRadar()
        {
            CalculateVectorToEnemies();
            ShowTargets();
        }

        private void CalculateVectorToEnemies()
        {
            foreach (RadarTarget radarTarget in _radarTargets)
            {
                if (radarTarget.target.IsLive)
                {
                    radarTarget.VectorToTarget = radarTarget.target.Transform.position - _player.transform.position;
                }
                else
                {
                    _radarTargets.Remove(radarTarget);
                    Destroy(radarTarget.gameObject);
                }
            }
        }

        private void ShowTargets()
        {
            foreach (RadarTarget radarTarget in _radarTargets)
            {
                Vector3 targetPosition = radarTarget.VectorToTarget / _radarScaling;
                radarTarget.transform.localPosition = targetPosition;
            }
        }
    }
}