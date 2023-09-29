using SpaceShooter.Spaceships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI.Destroyer
{
    public class DestroyerRadar : MonoBehaviour
    {
        [SerializeField]
        private DestroyerStatus _playerDestroyer;
        [SerializeField]
        private List<DestroyerRadarTarget> _destroyerRadarTargets;

        [SerializeField]
        private DestroyerRadarTarget _radarTargetPrefab;

        public void AddTarget(DestroyerStatus destroyer)
        {
            DestroyerRadarTarget target = Instantiate(_radarTargetPrefab, transform.position, Quaternion.identity, transform);
            target.SetTarget(destroyer);
            _destroyerRadarTargets.Add(target);
        }

        private void Update()
        {
            foreach(DestroyerRadarTarget destroyerRadarTarget in _destroyerRadarTargets)
            {
                if(destroyerRadarTarget.IsTargetLive)
                {
                    Vector3 vectorToTarget = destroyerRadarTarget.Target.transform.position - _playerDestroyer.transform.position;
                    destroyerRadarTarget.transform.position = transform.position + (vectorToTarget /100f).normalized/2;
                }
                else
                {
                    _destroyerRadarTargets.Remove(destroyerRadarTarget);
                    Destroy(destroyerRadarTarget);
                }
            }
        }
    }

    
}