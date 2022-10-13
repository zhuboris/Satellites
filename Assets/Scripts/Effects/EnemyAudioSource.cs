using Gameplay.Enemy;
using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemyAudioSource : MonoBehaviour
    {
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            EnemyHealth.DestroyedByPlayer += OnEnemyDestroyed;
            EnemyHealth.DestroyedByEnemy += OnEnemyDestroyed;
        }

        private void OnDisable()
        {
            EnemyHealth.DestroyedByPlayer -= OnEnemyDestroyed;
            EnemyHealth.DestroyedByEnemy -= OnEnemyDestroyed;
        }

        private void OnEnemyDestroyed()
        {
            _source.Play();
        }
    }
}