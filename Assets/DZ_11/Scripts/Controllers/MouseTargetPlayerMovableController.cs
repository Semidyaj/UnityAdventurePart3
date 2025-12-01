using UnityEngine;

namespace DZ_11
{
    public class MouseTargetPlayerMovableController : Controller
    {
        private IDirectionalMovable _player;

        private LayerMask _groundMask;
        private Ray _ray;

        private Vector3 _currentDirection;
        private Vector3 _currentTarget;

        public MouseTargetPlayerMovableController(IDirectionalMovable player, LayerMask groundMask)
        {
            _player = player;
            _groundMask = groundMask;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentDirection = SetTargetPosition().normalized;

                _player.SetMoveDirection(_currentDirection);
            }

            float distanceToTarget = (_currentTarget - _player.CurrentPosition).magnitude;

            if (distanceToTarget < 0.1f)
                _player.StopMove();
        }

        private Vector3 SetTargetPosition()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit groundHit, 100f, _groundMask))
            {
                _currentTarget = groundHit.point;
                return groundHit.point - _player.CurrentPosition;
            }

            return _player.CurrentPosition;
        }
    }
}