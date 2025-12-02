using System;
using System.Data;
using UnityEngine;

namespace DZ_11
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float _timeToHit;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _damageMask;

        private Vector3 _trapHitSize = new Vector3(2, 2, 2);

        private float _timer;

        private bool _isTriggered;

        private void Update()
        {
            if (_isTriggered)
            {
                _timer += Time.deltaTime;

                if (_timer >= _timeToHit)
                {
                    ApplyDamage();
                    _timer = 0;
                    _isTriggered = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
                _isTriggered = true;
        }

        private void ApplyDamage()
        {
            Collider[] hits = Physics.OverlapBox(transform.position, _trapHitSize / 2, transform.rotation, _damageMask);

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent<IDamagable>(out IDamagable damagable))
                    damagable.TakeDamage(_damage);
            }
        }

        private void OnDrawGizmos()
        {
            if (_isTriggered)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position, _trapHitSize);
            }
        }
    }
}