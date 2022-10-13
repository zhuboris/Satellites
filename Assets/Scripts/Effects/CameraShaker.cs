using Gameplay.Player;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _duration;

        private void OnEnable()
        {
            PlayerHealth.Damaged += StartShake;
        }

        private void OnDisable()
        {
            PlayerHealth.Damaged -= StartShake;
        }

        private void StartShake()
        {
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            var startPosition = transform.localPosition;
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                float strength = _curve.Evaluate(elapsedTime / _duration);
                transform.localPosition = startPosition + Random.insideUnitSphere * strength;
                yield return null;
            }

            transform.localPosition = startPosition;
        }
    }
}