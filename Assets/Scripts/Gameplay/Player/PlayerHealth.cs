using System;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(PlayerPhysics))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _startHealth;
        [SerializeField] private ParticleSystem _hitVfx;
        [SerializeField] private ParticleSystem _destructionVfx;

        private PlayerPhysics _player;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _collider;
        private float _health;
        private bool _isDead = false;

        private float HealthInPercents => _health / _startHealth;

        public static event Action Damaged;
        public static event Action<float> Changed;
        public static event Action<float> Reseted;
        public static event Action Died;

        private void Awake()
        {
            _player = GetComponent<PlayerPhysics>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();
        }

        private void OnEnable()
        {
            Game.Started += OnGameStarted;
        }

        private void OnDisable()
        {
            Game.Started -= OnGameStarted;
        }

        public void TakeHit(Vector2 force, Vector2 collisionPoint)
        {
            TakeDamage(force.magnitude);
            Damaged?.Invoke();

            if (_isDead == false)
            {
                PlayVfx(_hitVfx, collisionPoint);
                _player.AddForce(force);
            }
            else
            {
                PlayVfx(_destructionVfx, transform.position);
            }
        }

        private void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health < 0)
            {
                Die();
            }

            Changed?.Invoke(HealthInPercents);
        }

        private void PlayVfx(ParticleSystem vfx, Vector2 playingPoint)
        {
            vfx.transform.position = playingPoint;
            vfx.Play();
        }

        private void Die()
        {
            _isDead = true;
            _health = 0;
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
            Died?.Invoke();
        }

        private void OnGameStarted()
        {
            _isDead = false;
            _health = _startHealth;
            _spriteRenderer.enabled = true;
            _collider.enabled = true;
            Reseted?.Invoke(HealthInPercents);
        }
    }
}