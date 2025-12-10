using UnityEngine;
using UnityEngine.AI;

namespace DZ_11
{
    public class AgentPlayer : MonoBehaviour, ITransformPosition, IHealable, IDamagable
    {
        [SerializeField] private float _maxHealth;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private NavMeshAgent _agent;

        private AgentMover _mover;
        private DirectionalRotator _rotator;

        private Health _health;

        public Vector3 CurrentPosition => transform.position;
        public Vector3 CurrentVelocity => _mover.CurrentVelocity;
        public Quaternion CurrentRotation => _rotator.CurrentRotation;

        public float CurrentHealth => _health.CurrentHealth;
        public float MaxHealth => _health.MaxHealth;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;

            _mover = new AgentMover(_agent, _moveSpeed);
            _rotator = new DirectionalRotator(transform, _rotationSpeed);

            _health = new Health(_maxHealth);
        }

        private void Update()
        {
            _rotator.Update(Time.deltaTime);
        }

        public void SetDestination(Vector3 position) => _mover.SetDestination(position);

        public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

        public void StopMove() => _mover.Stop();

        public void ResumeMove() => _mover.Resume();

        public void Heal(float additiveHealth) => _health.Heal(additiveHealth);

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public bool TryGetPath(Vector3 targetPosition, NavMeshPath path) => NavMeshUtils.TryGetPath(_agent, targetPosition, path);
    }
}