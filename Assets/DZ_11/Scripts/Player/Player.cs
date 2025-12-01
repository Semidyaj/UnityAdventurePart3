using UnityEngine;

namespace DZ_11
{
    public class Player : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private DirectionalMover _mover;
        private DirectionalRotator _rotator;

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _mover = new DirectionalMover(_characterController, _moveSpeed);
            _rotator = new DirectionalRotator(transform, _rotationSpeed);
        }

        public Vector3 CurrentVelocity => _mover.CurrentVelocity;
        public Vector3 CurrentPosition => transform.position;
        public Quaternion CurrentRotation => _rotator.CurrentRotation;

        private void Update()
        {
            _mover.Update(Time.deltaTime);
            _rotator.Update(Time.deltaTime);
        }

        public void StopMove() => _mover.StopMove();

        public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);

        public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
    }
}