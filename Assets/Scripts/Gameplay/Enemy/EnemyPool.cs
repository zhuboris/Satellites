using System.Linq;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyPool
    {
        private EnemyMovement[] _pool;

        public void Init(EnemyMovement prefab, int count, Transform container)
        {
            _pool = new EnemyMovement[count];

            for (int i = 0; i < _pool.Length; i++)
            {
                var enemy = Object.Instantiate(prefab, container);
                enemy.gameObject.SetActive(false);

                _pool[i] = enemy;
            }
        }

        public bool TryGetObject(out EnemyMovement result)
        {
            result = _pool.FirstOrDefault(enemy => enemy.gameObject.activeSelf == false);
            return result != null;
        }

        public void ResetPool()
        {
            foreach (EnemyMovement enemy in _pool)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
}