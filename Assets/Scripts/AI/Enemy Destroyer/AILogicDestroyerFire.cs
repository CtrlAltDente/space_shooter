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
        private DestroyerStatus _closestDestroyer;
        [SerializeField]
        List<DestroyerStatus> _contactedDestroyers;

        [SerializeField]
        private DestroyerFireSystemLogic _destroyerFireSystemLogic;

        private RaycastHit[] _hits;

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
            _hits = Physics.SphereCastAll(_aiDestroyer.transform.position + _aiDestroyer.transform.forward * 10f, 10f, _aiDestroyer.transform.forward, 1000);

            _contactedDestroyers = new List<DestroyerStatus>();

            foreach (RaycastHit hit in _hits)
            {
                if (hit.transform.GetComponent<DestroyerStatus>())
                {
                    _contactedDestroyers.Add(hit.transform.GetComponent<DestroyerStatus>());
                }
            }

            Debug.Log("C: " + _contactedDestroyers.Count);

            if (_contactedDestroyers.Contains(_aiDestroyer))
            {
                _contactedDestroyers.Remove(_aiDestroyer);
            }

            if (_contactedDestroyers.Count > 0)
            {
                _closestDestroyer = _contactedDestroyers[0];

                foreach (DestroyerStatus destroyer in _contactedDestroyers)
                {
                    if (Vector3.Distance(_closestDestroyer.transform.position, transform.position) > Vector3.Distance(destroyer.transform.position, transform.position))
                    {
                        _closestDestroyer = destroyer;
                    }
                }

                if (_closestDestroyer == _playerDestroyer)
                {
                    _fireInput = true;
                }
                else
                {
                    _fireInput = false;
                }
            }
            else
            {
                _fireInput = false;
            }
        }
    }
}