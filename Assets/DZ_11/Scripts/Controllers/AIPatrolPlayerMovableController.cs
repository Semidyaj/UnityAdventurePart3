using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class AIPatrolPlayerMovableController : Controller
    {
        private const int MinCornersCountInPathToMove = 2;
        private const int StartCornerIndex = 0;
        private const int TargetCornerIndex = 1;

        private IDirectionalMovable _player;

        private Vector3 _currentTarget;
        private float _radius;

        private float _minDistanceToSwapTarget = 0.1f;
        private float _distanceToStop = 0.05f;

        private float _idleTimeToSwap = 1f;
        private float _idleTime;

        private bool _isIdle;

        private int _attempts = 10;

        private NavMeshQueryFilter _queryFilter;
        private NavMeshPath _path = new NavMeshPath();

        public AIPatrolPlayerMovableController(IDirectionalMovable player, float radius, NavMeshQueryFilter queryFilter)
        {
            _player = player;
            _radius = radius;
            _queryFilter = queryFilter;
        }

        public override void Enable()
        {
            base.Enable();

            SetTargetPosition();
            _idleTime = 0f;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            PlayPauseBetweenMoving(deltaTime);

            if (_isIdle)
                return;

            MoveToTarget();
        }

        private void MoveToTarget()
        {
            if (_currentTarget != _player.CurrentPosition)
            {
                if (NavMeshUtils.TryGetPath(_player.CurrentPosition, _currentTarget, _queryFilter, _path))
                {
                    float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                    if (EnoughCornersInPath(_path))
                    {
                        _player.SetMoveDirection((_path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex]).normalized);

                        if (NavMeshUtils.GetPathLength(_path) < _distanceToStop)
                            _player.StopMove();
                    }
                    else
                    {
                        SetTargetPosition();
                    }
                }
            }
        }

        private void PlayPauseBetweenMoving(float deltaTime)
        {
            if (Vector3.Distance(_player.CurrentPosition, _currentTarget) < _minDistanceToSwapTarget)
            {
                if (_isIdle == false)
                {
                    _isIdle = true;
                    _idleTime = 0f;
                    _player.StopMove();
                }

                _idleTime += deltaTime;

                if (_idleTime >= _idleTimeToSwap)
                {
                    _isIdle = false;
                    SetTargetPosition();
                }
            }
        }

        private void SetTargetPosition()
        {
            for (int i = 0; i < _attempts; i++)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-_radius, _radius), 0, Random.Range(-_radius, _radius));

                Vector3 rawTarget = _player.CurrentPosition + randomDirection;

                if (NavMesh.SamplePosition(rawTarget, out NavMeshHit hit, _radius, _queryFilter.areaMask))
                {
                    _currentTarget = hit.position;
                    return;
                }
            }
        }

        private bool EnoughCornersInPath(NavMeshPath pathToTarget) => pathToTarget.corners.Length >= MinCornersCountInPathToMove;
    }
}