using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class GameManager : MonoBehaviour
    {
        private const float MinVelocityToStartTimer = 0.05f;
        private const int LeftMouseButtonKey = 0;

        [SerializeField] private HealthUI _healthUI;

        [SerializeField] private Player _player;
        [SerializeField] private AgentPlayer _agentPlayer;
        [SerializeField] private LayerMask _groundMask;

        [SerializeField] private float _timeToStartPatrol;
        [SerializeField] private float _radiusToPatrol;

        //private Controller _playerController;
        //private Controller _playerAlternateController;
        private Controller _agentController;
        private Controller _agentAlternateController;

        private float _time;

        private float _testDamage = 50f;

        private void Awake()
        {
            //NavMeshQueryFilter filter = new NavMeshQueryFilter();
            //filter.agentTypeID = 0;
            //filter.areaMask = NavMesh.AllAreas;

            //_playerController = new CompositeController(
            //    new MouseTargetPlayerMovableController(_player, _groundMask, filter),
            //    new AlongMovableVelocityRotatableController(_player, _player));

            //_playerAlternateController = new CompositeController(
            //    new AIPatrolPlayerMovableController(_player, _radiusToPatrol, filter),
            //    new AlongMovableVelocityRotatableController(_player, _player));

            //_playerController.Enable();
            //_playerAlternateController.Disable();

            _agentController = new CompositeController(
                new AgentMouseTargetPlayerMovableController(_agentPlayer, _groundMask),
                new AgentRotatableController(_agentPlayer));

            _agentAlternateController = new CompositeController(
                new AgentAIPatrolMovableController(_agentPlayer, _radiusToPatrol),
                new AgentRotatableController(_agentPlayer));

            _agentController.Enable();
            _agentAlternateController.Disable();
        }

        private void Start()
        {
            //_healthUI.Initialize(_player, _player.MaxHealth);
            _healthUI.Initialize(_agentPlayer, _agentPlayer.MaxHealth);
        }

        private void Update()
        {
            //if (_player.CurrentHealth == 0)
            //{
            //    _playerController.Disable();
            //    _playerAlternateController.Disable();
            //}

            //if (_player.CurrentVelocity.magnitude < MinVelocityToStartTimer)
            //    _time += Time.deltaTime;
            //else
            //    _time = 0;

            //if (_time >= _timeToStartPatrol && _playerAlternateController.IsEnabled == false)
            //{
            //    _playerController.Disable();
            //    _playerAlternateController.Enable();
            //}

            //if (Input.GetMouseButtonDown(LeftMouseButtonKey) && _playerController.IsEnabled == false)
            //{
            //    _playerAlternateController.Disable();
            //    _playerController.Enable();
            //}

            //if (_playerController.IsEnabled)
            //    _playerController.Update(Time.deltaTime);
            //else if (_playerAlternateController.IsEnabled)
            //    _playerAlternateController.Update(Time.deltaTime);

            if (_agentPlayer.CurrentHealth == 0)
                _agentController.Disable();

            if (Input.GetKeyDown(KeyCode.Space))
                _agentPlayer.TakeDamage(_testDamage);

            if (_agentPlayer.CurrentVelocity.magnitude < MinVelocityToStartTimer)
                _time += Time.deltaTime;
            else
                _time = 0;

            if (_time >= _timeToStartPatrol && _agentAlternateController.IsEnabled == false)
            {
                _agentController.Disable();
                _agentAlternateController.Enable();
                _time = 0f;
            }

            if (Input.GetMouseButtonDown(LeftMouseButtonKey) && _agentController.IsEnabled == false)
            {
                _agentAlternateController.Disable();
                _agentController.Enable();
                _time = 0f;
            }

            if (_agentController.IsEnabled)
                _agentController.Update(Time.deltaTime);
            else if (_agentAlternateController.IsEnabled)
                _agentAlternateController.Update(Time.deltaTime);
        }
    }
}