using Gameplay.Player;
using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destroyVfx;

        private EnemyMovement _movement;
        private bool didHitPlayer = false;
        private bool didHitEnemy = false;

        public static event Action DestroyedByPlayer;
        public static event Action DestroyedByEnemy;
        public static event Action DestroyedWithMissingTarget;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovement>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionPoint = collision.ClosestPoint(transform.position);
            SpawnVfx(collisionPoint);

            didHitPlayer = collision.TryGetComponent(out PlayerHealth player);

            if (didHitPlayer)
            {
                var hitForce = _movement.GetMomentum();
                player.TakeHit(hitForce, collisionPoint);
            }
            else
            {
                didHitEnemy = collision.TryGetComponent(out EnemyHealth _);
            }

            gameObject.SetActive(false);
        }

        private void OnBecameInvisible()
        {

            if (didHitPlayer)
            {
                DestroyedByPlayer?.Invoke();
                didHitPlayer = false;
                return;
            }

            if (Game.IsActive == false)
            {
                return;
            }

            if (didHitEnemy)
            {
                DestroyedByEnemy?.Invoke();
                didHitEnemy = false;
                return;
            }

            DestroyedWithMissingTarget?.Invoke();
            gameObject.SetActive(false);
        }

        private void SpawnVfx(Vector2 spawnPoint)
        {
            Instantiate(_destroyVfx, spawnPoint, Quaternion.identity, transform.parent);
        }
    }
}