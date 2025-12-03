using UnityEngine;

namespace DZ_11
{
    public class TrapView : MonoBehaviour
    {
        private readonly int IsTriggeredKey = Animator.StringToHash("IsTriggered");

        private Animator _animator;
        private Trap _trap;

        private void Awake()
        {
            _trap = GetComponentInParent<Trap>();
            _animator= GetComponent<Animator>();
        }

        private void Update()
        {
            if (_trap.CanPlayHitAnimation)
                SpikesHit();
        }

        private void SpikesHit() => _animator.SetTrigger(IsTriggeredKey);
    }
}