using System.Collections;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _prefab;
        [SerializeField] private Transform _player;
        [SerializeField] private int _enemyPoolCapacity = 15;
        [SerializeField] private float _spawnDelay = 1f;
        [SerializeField] private float _spawnRate = 3f;
        [SerializeField] private float _minSpawnDistanceToPlayer = 5f;

        private float _spawnDistanceFromCenter;
        private EnemyPool _enemyPool;
        private Coroutine _spawnCoroutine;
        private WaitForSeconds _waitForSpawnDelay;
        private WaitForSeconds _waitForSpawnRate;

        private void Awake()
        {
            _enemyPool = new();
            _enemyPool.Init(_prefab, _enemyPoolCapacity, transform);
            _waitForSpawnDelay = new(_spawnDelay);
            _waitForSpawnRate = new(_spawnRate);

            _spawnDistanceFromCenter = GetDistanceFromScreenCenterToCorner();
        }

        private void OnEnable()
        {
            Game.Started += OnGameStarted;
        }

        private void OnDisable()
        {
            Game.Started -= OnGameStarted;
        }

        private IEnumerator SpawnRepeating()
        {
            yield return _waitForSpawnDelay;

            while (Game.IsActive)
            {
                SpawnOne();
                yield return _waitForSpawnRate;
            }
        }

        private void SpawnOne()
        {
            bool isSpawned = _enemyPool.TryGetObject(out EnemyMovement enemy);

            if (isSpawned)
            {
                Vector2 spawnPoint = GetSpawnPoint();
                enemy.Init(spawnPoint, _player);
            }
        }

        private float GetDistanceFromScreenCenterToCorner()
        {
            var camera = Camera.main;
            var topRightCameraCorner = (Vector2)camera.ScreenToWorldPoint(new(camera.pixelWidth, camera.pixelHeight, camera.nearClipPlane));
            return topRightCameraCorner.magnitude;
        }

        private Vector2 GetSpawnPoint()
        {
            var result = Random.insideUnitCircle.normalized * _spawnDistanceFromCenter + (Vector2)_player.position;
            float distanceToPlayer = Vector2.Distance(_player.position, result);
            
            if (distanceToPlayer < _minSpawnDistanceToPlayer)
            {
                result = (-1) * result;
            }

            return result;
        }

        private void OnGameStarted()
        {
            _enemyPool.ResetPool();

            if (_spawnCoroutine is not null)
            {
                StopCoroutine(SpawnRepeating());
            }

            _spawnCoroutine = StartCoroutine(SpawnRepeating());
        }
    }
}