using UnityEngine;

namespace DZ_11
{
    public class PlayerView : MonoBehaviour
    {
        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsDieKey = Animator.StringToHash("IsDie");
        private readonly int IsHittingKey = Animator.StringToHash("IsHitting");

        private const float MinVelocityToStartRunningAnimation = 0.05f;

        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;

        [SerializeField] private float _injuredHealthPercent;

        private int _injuredLayerIndex = 1;

        private float _previousFrameHealth;

        private void Start()
        {
            _previousFrameHealth = _player.MaxHealth;
        }

        private void Update()
        {
            if (_player.CurrentHealth / _player.MaxHealth * 100 <= _injuredHealthPercent)
                _animator.SetLayerWeight(_injuredLayerIndex, 1f);
            else
                _animator.SetLayerWeight(_injuredLayerIndex, 0f);

            if (_player.CurrentHealth <= 0)
                Die();

            if (_player.CurrentHealth < _previousFrameHealth)
            {
                TakeHit();
            }

            _previousFrameHealth = _player.CurrentHealth;

            if (_player.CurrentVelocity.magnitude > MinVelocityToStartRunningAnimation)
                StartRunning();
            else
                StopRunning();
        }

        private void Die() => _animator.SetBool(IsDieKey, true);

        private void TakeHit() => _animator.SetTrigger(IsHittingKey);

        private void StartRunning() => _animator.SetBool(IsRunningKey, true);

        private void StopRunning() => _animator.SetBool(IsRunningKey, false);
    }
}