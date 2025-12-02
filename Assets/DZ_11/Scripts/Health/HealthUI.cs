using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DZ_11
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image _filledImage;
        [SerializeField] private TextMeshProUGUI _healthCounterText;

        [SerializeField] private Player _player;

        private float _maxHealth;
        private float _currentHealth;

        private void Start()
        {
            _maxHealth = _player.MaxHealth;
        }

        private void Update()
        {
            _currentHealth = _player.CurrentHealth;

            _filledImage.fillAmount = _currentHealth / _maxHealth;

            _healthCounterText.text = _currentHealth.ToString();
        }
    }
}