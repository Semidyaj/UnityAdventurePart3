using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DZ_11
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image _filledImage;
        [SerializeField] private TextMeshProUGUI _healthCounterText;

        private IHealable _healable;

        private float _maxHealth;
        private float _currentHealth;

        private void Update()
        {
            _currentHealth = _healable.CurrentHealth;

            _filledImage.fillAmount = _currentHealth / _maxHealth;

            _healthCounterText.text = _currentHealth.ToString();
        }

        public void Initialize(IHealable healable, float maxHealth)
        {
            _healable = healable;
            _maxHealth = maxHealth;
        }
    }
}