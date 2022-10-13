using Gameplay.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class PlayerHealthbar : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Slider _slider;
        private Image[] _images;
        private Coroutine _coroutine;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _images = GetComponentsInChildren<Image>();
            ChangeVisibility(false);
        }

        private void OnEnable()
        {
            PlayerHealth.Changed += OnHealthChanged;
            PlayerHealth.Reseted += OnHealthReseted;
        }

        private void OnDisable()
        {
            PlayerHealth.Changed -= OnHealthChanged;
            PlayerHealth.Reseted -= OnHealthReseted;
        }

        private void OnHealthChanged(float value)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(nameof(ChangeValue), value);
        }

        private void OnHealthReseted(float value)
        {
            _slider.value = value * _slider.maxValue;

            ChangeVisibility(true);
        }

        private IEnumerator ChangeValue(float value)
        {
            float sliderValue = value * _slider.maxValue;

            while (_slider.value != sliderValue)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, sliderValue, _speed * Time.deltaTime);
                yield return null;
            }

            if (_slider.value == 0)
            {
                ChangeVisibility(false);
            }
        }

        private void ChangeVisibility(bool isVisible)
        {
            foreach (Image image in _images)
            {
                image.enabled = isVisible;
            }
        }
    }
}