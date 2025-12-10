using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class AgentAIPatrolMovableController : Controller
    {
        private AgentPlayer _agentPlayer;

        private Vector3 _currentTarget;
        private float _radius;

        private float _minDistanceToSwapTarget = 0.3f;
        private float _distanceToStop = 0.3f;

        private float _idleTimeToSwap = 1f;
        private float _idleTime;

        private bool _isIdle;

        private int _attempts = 10;

        private NavMeshPath _path = new NavMeshPath();

        public AgentAIPatrolMovableController(AgentPlayer agentPlayer, float radius)
        {
            _agentPlayer = agentPlayer;
            _radius = radius;
        }

        public override void Enable()
        {
            base.Enable();

            _agentPlayer.ResumeMove();

            SetTargetPosition();
            _idleTime = 0f;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            float distance = Vector3.Distance(_agentPlayer.CurrentPosition, _currentTarget);

            if (distance < _minDistanceToSwapTarget)
            {
                PlayPauseBetweenMoving(deltaTime);
                return;
            }

            _isIdle = false;

            MoveToTarget();
        }

        private void MoveToTarget()
        {
            if (_agentPlayer.TryGetPath(_currentTarget, _path))
            {
                _agentPlayer.ResumeMove();
                _agentPlayer.SetDestination(_currentTarget);
            }
            else
            {
                SetTargetPosition();
            }
        }

        private void PlayPauseBetweenMoving(float deltaTime)
        {
            if (_isIdle == false)
            {
                _isIdle = true;
                _idleTime = 0f;
                _agentPlayer.StopMove();
            }

            _idleTime += deltaTime;

            if (_idleTime >= _idleTimeToSwap)
            {
                _isIdle = false;
                SetTargetPosition();
            }
        }

        private void SetTargetPosition()
        {
            for (int i = 0; i < _attempts; i++)
            {
                Vector3 randomDirection = Random.insideUnitSphere * _radius;
                randomDirection.y = 0;

                Vector3 rawTarget = _agentPlayer.CurrentPosition + randomDirection;

                if (NavMesh.SamplePosition(rawTarget, out NavMeshHit hit, _radius, NavMesh.AllAreas))
                {
                    if (Vector3.Distance(_agentPlayer.CurrentPosition, hit.position) > _minDistanceToSwapTarget * 2f)
                    {
                        _currentTarget = hit.position;
                        return;
                    }
                }
            }
        }
    }
}