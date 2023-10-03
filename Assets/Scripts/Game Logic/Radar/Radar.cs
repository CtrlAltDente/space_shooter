using SpaceShooter.Spaceships;
using SpaceShooter.UI.Destroyer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.GameLogic
{
    public class Radar : MonoBehaviour
    {
        [SerializeField]
        private DestroyerStatus _player;
        [SerializeField]
        private RadarTarget _radarTargetPrefab;
        [SerializeField]
        private List<RadarTarget> _radarTargets = new List<RadarTarget>();

        [SerializeField]
        private List<DestroyerStatus> _destroyersToAdd = new List<DestroyerStatus>();

        private void Awake()
        {
            foreach(DestroyerStatus destroyer in _destroyersToAdd)
            {
                AddEnemyDestroyers(destroyer);
            }
        }

        private void Update()
        {
            ShowDestroyersOnRadar();
        }

        public void AddEnemyDestroyers(DestroyerStatus destroyerStatus)
        {
            RadarTarget newRadarTarget = Instantiate(_radarTargetPrefab, transform.position, Quaternion.identity, transform);
            newRadarTarget.SetTarget(destroyerStatus);
            _radarTargets.Add(newRadarTarget);
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
                if (radarTarget.EnemyDestroyer.IsLive)
                {
                    radarTarget.VectorToTarget = (radarTarget.EnemyDestroyer.transform.position - _player.transform.position);
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
            foreach(RadarTarget radarTarget in _radarTargets)
            {
                radarTarget.transform.localPosition = radarTarget.VectorToTarget / 1000;
            }
        }
    }
}