using UnityEngine;

namespace DZ_11
{
    public class PlayerView : MonoBehaviour
    {
        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsDieKey = Animator.StringToHash("IsDie");

        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;

        private void Update()
        {
            if (_player.CurrentVelocity.magnitude > 0.1f)
                StartRunning();
            else
                StopRunning();
        }

        private void StartRunning()
        {
            _animator.SetBool(IsRunningKey, true);
        }

        private void StopRunning()
        {
            _animator.SetBool(IsRunningKey, false);
        }
    }
}