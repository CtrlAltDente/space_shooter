using SpaceShooter.Interfaces;
using SpaceShooter.Spaceships;
using SpaceShooter.Spaceships.Destroyer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.AI.Destroyer
{
    public class AILogicDestroyerFire : MonoBehaviour, IFireInput
    {
        [SerializeField]
        private DestroyerStatus _playerDestroyer;

        [SerializeField]
        private DestroyerStatus _aiDestroyer;

        [SerializeField]
        private bool _fireInput = false;
        public bool FireInput
        {
            get
            {
                return _fireInput;
            }
        }

        private void Update()
        {
            AttackPlayer();
        }

        private void AttackPlayer()
        {
            List<DestroyerStatus> destroyers = FindDestroyers();

            if (FindClosestDestroyer(destroyers) == _playerDestroyer)
            {
                _fireInput = true;
            }
            else
            {
                _fireInput = false;
            }
        }

        private List<DestroyerStatus> FindDestroyers()
        {
            RaycastHit[] hits = Physics.SphereCastAll(_aiDestroyer.transform.position + _aiDestroyer.transform.forward * 10f, 5f, _aiDestroyer.transform.forward, 1000);

            List<DestroyerStatus> findedDestroyersAtForward = new List<DestroyerStatus>();

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<DestroyerStatus>())
                {
                    findedDestroyersAtForward.Add(hit.transform.GetComponent<DestroyerStatus>());
                }
            }

            if (findedDestroyersAtForward.Contains(_aiDestroyer))
            {
                findedDestroyersAtForward.Remove(_aiDestroyer);
            }

            return findedDestroyersAtForward;
        }

        private DestroyerStatus FindClosestDestroyer(List<DestroyerStatus> destroyers)
        {
            DestroyerStatus closestDestroyer = null;

            if (destroyers.Count > 0)
            {
                closestDestroyer = destroyers[0];

                foreach (DestroyerStatus destroyer in destroyers)
                {
                    if (Vector3.Distance(closestDestroyer.transform.position, transform.position) > Vector3.Distance(destroyer.transform.position, transform.position))
                    {
                        closestDestroyer = destroyer;
                    }
                }
            }

            return closestDestroyer;
        }
    }
}