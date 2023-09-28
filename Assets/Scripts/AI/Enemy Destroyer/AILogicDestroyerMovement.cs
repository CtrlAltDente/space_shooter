using SpaceShooter.Spaceships.Destroyer;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Spaceships;

namespace SpaceShooter.AI.Destroyer
{
    public class AILogicDestroyerMovement : MonoBehaviour, IMovementInput
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
                Vector3 targetPosition;

                if ((_isTooFarFromPlayer || !_isTooCloseToPlayer) && !_flyAway)
                {
                    targetPosition = _playerDestroyer.transform.position;
                }
                else
                {
                    targetPosition = _flyAwayPosition;
                }

                int xDirection = CalculateNearAxisPositionToTarget(targetPosition, _aiDestroyer.transform.right);
                int yDirection = -CalculateNearAxisPositionToTarget(targetPosition, _aiDestroyer.transform.up);

                direction = new Vector2(xDirection, yDirection);

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

        private int CalculateNearAxisPositionToTarget(Vector3 targetPosition, Vector3 axis)
        {
            Vector3 leftSide = _aiDestroyer.transform.position + -axis;
            Vector3 rightSide = _aiDestroyer.transform.position + axis;

            Debug.DrawLine(leftSide, targetPosition, Color.red);
            Debug.DrawLine(rightSide, targetPosition, Color.green);

            int xDirection = (targetPosition - leftSide).sqrMagnitude > (targetPosition - rightSide).sqrMagnitude ? 1 : -1;

            return xDirection;
        }

        private IEnumerator DoAiLogic()
        {
            while (_playerDestroyer.IsLive && _aiDestroyer.IsLive)
            {
                yield return new WaitForSeconds(10f);
                CalculateFlyAwayPosition();
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