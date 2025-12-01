using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class MouseTargetPlayerMovableController : Controller
    {
        private const int MinCornersCountInPathToMove = 2;
        private const int StartCornerIndex = 0;
        private const int TargetCornerIndex = 1;

        private IDirectionalMovable _player;

        private LayerMask _groundMask;
        private Ray _ray;

        private Vector3 _currentTarget;

        private NavMeshQueryFilter _queryFilter;
        private NavMeshPath _path = new NavMeshPath();

        public MouseTargetPlayerMovableController(IDirectionalMovable player, LayerMask groundMask, NavMeshQueryFilter queryFilter)
        {
            _player = player;
            _groundMask = groundMask;
            _queryFilter = queryFilter;
            _currentTarget = _player.CurrentPosition;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
                SetTargetPosition();

            if (_currentTarget != _player.CurrentPosition)
            {
                if (NavMeshUtils.TryGetPath(_player.CurrentPosition, _currentTarget, _queryFilter, _path))
                {
                    float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                    if (EnoughCornersInPath(_path))
                    {
                        _player.SetMoveDirection((_path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex]).normalized);

                        if (NavMeshUtils.GetPathLength(_path) < 0.1f)
                            _player.StopMove();
                    }
                }
            }
        }

        private bool EnoughCornersInPath(NavMeshPath pathToTarget) => pathToTarget.corners.Length >= MinCornersCountInPathToMove;

        private void SetTargetPosition()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit groundHit, 100f, _groundMask))
                _currentTarget = groundHit.point;
        }
    }
}