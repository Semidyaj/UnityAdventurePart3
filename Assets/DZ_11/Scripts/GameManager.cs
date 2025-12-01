using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private LayerMask _groundMask;

        private Controller _playerController;

        private void Awake()
        {
            NavMeshQueryFilter filter = new NavMeshQueryFilter();
            filter.agentTypeID = 0;
            filter.areaMask = NavMesh.AllAreas;

            _playerController = new CompositeController(
                new MouseTargetPlayerMovableController(_player, _groundMask, filter), 
                new AlongMovableVelocityRotatableController(_player, _player));

            _playerController.Enable();
        }

        private void Update()
        {
            _playerController.Update(Time.deltaTime);
        }
    }
}