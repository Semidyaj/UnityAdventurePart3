using UnityEngine;

namespace DZ_11
{
    public class Bottle : MonoBehaviour
    {
        [SerializeField] private int _additiveHealth;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IHealable>(out IHealable healable))
            {
                healable.Heal(_additiveHealth);
                Destroy(gameObject);
            }
        }
    }
}