using UnityEngine;

namespace DZ_11
{
    public class PlayerView : MonoBehaviour
    {
        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsDieKey = Animator.StringToHash("IsDie");

        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;

        [SerializeField] private float _injuredHealthPercent;

        private int _injuredLayerIndex = 1;

        private void Update()
        {
            if (_player.CurrentHealth / _player.MaxHealth * 100 <= _injuredHealthPercent)
                _animator.SetLayerWeight(_injuredLayerIndex, 1f);
            else
                _animator.SetLayerWeight(_injuredLayerIndex, 0f);

            if (_player.CurrentHealth <= 0)
                Die();

            if (_player.CurrentVelocity.magnitude > 0.1f)
                StartRunning();
            else
                StopRunning();
        }

        private void Die() => _animator.SetBool(IsDieKey, true);

        private void StartRunning() => _animator.SetBool(IsRunningKey, true);

        private void StopRunning() => _animator.SetBool(IsRunningKey, false);
    }
}