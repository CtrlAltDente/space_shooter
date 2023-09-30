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
        private List<RadarTarget> _radarTargets = new List<RadarTarget>();

        public void AddEnemyDestroyers(DestroyerStatus destroyerStatus)
        {
            _radarTargets.Add(Instantiate(new GameObject("Radar Target").AddComponent<RadarTarget>(), transform.position, Quaternion.identity, transform));
        }

        private void ShowDestroyersOnRadar()
        {
            CalculateVectorToEnemies();

        }

        private void CalculateVectorToEnemies()
        {
            foreach (RadarTarget radarTarget in _radarTargets)
            {
                if (radarTarget.EnemyDestroyer.IsLive)
                {
                    radarTarget.VectorToTarget = (radarTarget.EnemyDestroyer.transform.position - _playerDestroyer.transform.position);
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

                if(Vector3.Dot(_playerDestroyer.transform.forward, radarTarget.VectorToTarget.normalized) > 0.7f)
                {
                    Debug.Log("TRRR");
                }
            }
        }
    }

    public class RadarTarget : MonoBehaviour
    {
        public DestroyerStatus EnemyDestroyer { get; private set; }

        public Vector3 VectorToTarget;

        public void SetTarget(DestroyerStatus enemyDestroyer)
        {
            EnemyDestroyer = enemyDestroyer;
        }
    }
}