using UnityEngine;

namespace DZ_11
{
    public class CursorTargetSpawner : MonoBehaviour
    {
        [SerializeField] private Cursor _cursorPrefab;
        [SerializeField] private LayerMask _groundMask;

        private Cursor _currentTarget;

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
                if (_currentTarget != null)
                    Destroy(_currentTarget.gameObject);

                _currentTarget = Instantiate(_cursorPrefab, _spawnPosition, Quaternion.identity);

                _isGround = false;
            }
        }

        private void SetSpawnPosition()
        {
            if (Input.GetMouseButtonDown(0))
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