using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class AgentMouseTargetPlayerMovableController : Controller
    {
        private const int LeftMouseButtonKey = 0;

        private AgentPlayer _agentPlayer;

        private LayerMask _groundMask;
        private Ray _ray;

        private Vector3 _currentTarget;
        private float _distanceToStop = 0.05f;

        private NavMeshPath _path = new NavMeshPath();

        public AgentMouseTargetPlayerMovableController(AgentPlayer agentPlayer, LayerMask groundMask)
        {
            _agentPlayer = agentPlayer;
            _groundMask = groundMask;
            _currentTarget = _agentPlayer.CurrentPosition;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetMouseButtonDown(LeftMouseButtonKey))
                SetTargetPosition();

            if (_agentPlayer.TryGetPath(_currentTarget, _path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                _agentPlayer.ResumeMove();
                _agentPlayer.SetDestination(_currentTarget);

                if (distanceToTarget < _distanceToStop)
                    _agentPlayer.StopMove();
            }
        }

        private void SetTargetPosition()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit groundHit, 100f, _groundMask))
                _currentTarget = groundHit.point;
        }
    }
}