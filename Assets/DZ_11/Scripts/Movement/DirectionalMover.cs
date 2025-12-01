using UnityEngine;

namespace DZ_11
{
    public class DirectionalMover
    {
        private CharacterController _characterController;

        private float _movementSpeed;

        private Vector3 _currentDirection;

        public DirectionalMover(CharacterController characterController, float movementSpeed)
        {
            _characterController = characterController;
            _movementSpeed = movementSpeed;
        }

        public Vector3 CurrentVelocity { get; private set; }

        public void SetInputDirection(Vector3 direction) => _currentDirection = direction;

        public void Update(float deltaTime)
        {
            CurrentVelocity = _currentDirection * _movementSpeed;
            _characterController.Move(CurrentVelocity * deltaTime);
        }

        public void StopMove() => _currentDirection = Vector3.zero;
    }
}