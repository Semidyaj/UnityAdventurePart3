using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class GameManager : MonoBehaviour
    {
        private const float MinVelocityToStartTimer = 0.05f;
        private const int LeftMouseButtonKey = 0;

        [SerializeField] private Player _player;
        [SerializeField] private LayerMask _groundMask;

        [SerializeField] private float _timeToStartPatrol;
        [SerializeField] private float _radiusToPatrol;

        private Controller _playerController;
        private Controller _playerAlternateController;

        private float _time;

        private float _testDamage = 50f;

        private void Awake()
        {
            NavMeshQueryFilter filter = new NavMeshQueryFilter();
            filter.agentTypeID = 0;
            filter.areaMask = NavMesh.AllAreas;

            _playerController = new CompositeController(
                new MouseTargetPlayerMovableController(_player, _groundMask, filter),
                new AlongMovableVelocityRotatableController(_player, _player));

            _playerAlternateController = new CompositeController(
                new AIPatrolPlayerMovableController(_player, _radiusToPatrol, filter),
                new AlongMovableVelocityRotatableController(_player, _player));

            _playerController.Enable();
            _playerAlternateController.Disable();
        }

        private void Update()
        {
            if(_player.CurrentHealth == 0)
            {
                _playerController.Disable();
                _playerAlternateController.Disable();
            }

            if (Input.GetKeyDown(KeyCode.Space))
                _player.TakeDamage(_testDamage);

            if (_player.CurrentVelocity.magnitude < MinVelocityToStartTimer)
                _time += Time.deltaTime;
            else
                _time = 0;

            if (_time >= _timeToStartPatrol && _playerAlternateController.IsEnabled == false)
            {
                _playerController.Disable();
                _playerAlternateController.Enable();
            }
            
            if(Input.GetMouseButtonDown(LeftMouseButtonKey) && _playerController.IsEnabled == false) 
            {
                _playerAlternateController.Disable();
                _playerController.Enable();
            }

            if (_playerController.IsEnabled)
                _playerController.Update(Time.deltaTime);
            else if (_playerAlternateController.IsEnabled)
                _playerAlternateController.Update(Time.deltaTime);
        }
    }
}