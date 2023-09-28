using SpaceShooter.Spaceships.Destroyer;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Spaceships;

namespace SpaceShooter.AI.Destroyer
{
    public class AIDestroyerMovementController : MonoBehaviour, IDestroyerMovementInput
    {
        [SerializeField]
        private DestroyerStatus _playerDestroyer;

        [SerializeField]
        private DestroyerStatus _aiDestroyer;

        private bool _isTooFarFromPlayer => Vector3.Distance(_aiDestroyer.transform.position, _playerDestroyer.transform.position) > 200;
        private bool _isTooCloseToPlayer => Vector3.Distance(_aiDestroyer.transform.position, _playerDestroyer.transform.position) < 50;
        private bool _flyAway;

        private Vector3 _flyAwayPosition;

        private Coroutine _aiLogicCoroutine;

        public Vector2 DirectionInput
        {
            get
            {
                Vector2 direction;

                if ((_isTooFarFromPlayer || !_isTooCloseToPlayer) && !_flyAway)
                {
                    direction = new Vector2(CalculateNearXPositionToTarget(_playerDestroyer.transform.position), -CalculateNearYPositionToTarget(_playerDestroyer.transform.position));
                }
                else
                {
                    direction = new Vector2(CalculateNearXPositionToTarget(_flyAwayPosition), -CalculateNearYPositionToTarget(_flyAwayPosition));
                }
                Debug.Log(direction);

                return direction;
            }
        }

        public Vector2 RotationInput
        {
            get
            {
                return new Vector2();
            }
        }

        private void Start()
        {
            CalculateFlyAwayPosition();
            _aiLogicCoroutine = StartCoroutine(DoAiLogic());
        }

        public void CalculateInput()
        {

        }

        private int CalculateNearXPositionToTarget(Vector3 targetPosition)
        {
            Vector3 leftSide = _aiDestroyer.transform.position + -_aiDestroyer.transform.right * 10;
            Vector3 rightSide = _aiDestroyer.transform.position + _aiDestroyer.transform.right * 10;

            Debug.DrawLine(leftSide, targetPosition, Color.red);
            Debug.DrawLine(rightSide, targetPosition, Color.green);

            int xDirection = (targetPosition - leftSide).sqrMagnitude > (targetPosition - rightSide).sqrMagnitude ? 1 : -1;

            Debug.Log("Right " + ((targetPosition - leftSide).sqrMagnitude - (targetPosition - rightSide).sqrMagnitude));

            return xDirection;
        }

        private int CalculateNearYPositionToTarget(Vector3 targetPosition)
        {
            Vector3 bottomSide = _aiDestroyer.transform.position + -_aiDestroyer.transform.up * 10;
            Vector3 topSide = _aiDestroyer.transform.position + _aiDestroyer.transform.up * 10;

            Debug.DrawLine(bottomSide, targetPosition, Color.white);
            Debug.DrawLine(topSide, targetPosition, Color.yellow);

            int yDirection = (targetPosition - bottomSide).sqrMagnitude > (targetPosition - topSide).sqrMagnitude ? 1 : -1;

            Debug.Log("Up" + ((targetPosition - bottomSide).sqrMagnitude - (targetPosition - topSide).sqrMagnitude));

            return yDirection;
        }

        private IEnumerator DoAiLogic()
        {
            while (_playerDestroyer.IsLive && _aiDestroyer.IsLive)
            {
                yield return new WaitForSeconds(10f);
                CalculateFlyAwayPosition();
                _flyAway = !_flyAway;
            }
        }

        private void CalculateRotationToPlayer()
        {

        }

        private void CalculateFlyAwayPosition()
        {
            _flyAwayPosition = _playerDestroyer.transform.position + new Vector3(Random.Range(0f, 30f), Random.Range(0f, 30f), Random.Range(0f, 30f));
        }
    }
}