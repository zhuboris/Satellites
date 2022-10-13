using UnityEngine;
using Gameplay.Enemy;
using System;

namespace Gameplay.Player
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField] private int _bonusForClashOfEnemies = 5;

        private int _count;

        public static event Action<int> Changed;

        private void OnEnable()
        {
            Game.Started += ResetScore;
            EnemyHealth.DestroyedWithMissingTarget += OnEnemyMissed;
            EnemyHealth.DestroyedByEnemy += OnEnemyDestroyedByEnemy;
        }

        private void OnDisable()
        {
            Game.Started -= ResetScore;
            EnemyHealth.DestroyedWithMissingTarget -= OnEnemyMissed;
            EnemyHealth.DestroyedByEnemy += OnEnemyDestroyedByEnemy;
        }

        private void OnEnemyMissed()
        {
            _count++;
            Changed?.Invoke(_count);
        }

        private void OnEnemyDestroyedByEnemy()
        {
            _count += _bonusForClashOfEnemies;
            Changed?.Invoke(_count);
        }

        private void ResetScore()
        {
            _count = 0;
            Changed?.Invoke(_count);
        }
    }
}