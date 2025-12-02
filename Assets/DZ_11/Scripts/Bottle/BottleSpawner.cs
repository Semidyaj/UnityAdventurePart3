using UnityEngine;

namespace DZ_11
{
    public class BottleSpawner : MonoBehaviour
    {
        [SerializeField] private Bottle _bottlePrefab;
        [SerializeField] private LayerMask _groundMask;

        private Vector3 _spawnPosition;

        private Ray _ray;

        private bool _isGround;

        private void Update()
        {
            SetSpawnPosition();
            Spawn();
        }

        private void Spawn()
        {
            if (_isGround)
            {
                Instantiate(_bottlePrefab, _spawnPosition, Quaternion.identity);
                _isGround = false;
            }
        }

        private void SetSpawnPosition()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out RaycastHit groundHit, 100f, _groundMask))
                {
                    _spawnPosition = groundHit.point;
                    _isGround = true;
                }
            }
        }
    }
}