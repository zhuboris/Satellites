using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _stopFollowingDistance;

        private Transform _target;
        private Vector3 _direction;
        private float _speed;
        private bool _wasCloseToTarget = false;

        private void Update()
        {
            if (_wasCloseToTarget == false)
            {
                UpdateDirection();
                UpdateRotation();
            }

            transform.position += _speed * Time.deltaTime * _direction;
        }

        private void OnDisable()
        {
            _wasCloseToTarget = false;
        }

        public void Init(Vector2 spawnPosition, Transform target)
        {
            _target = target;
            _speed = Random.Range(_minSpeed, _maxSpeed);
            transform.position = spawnPosition;
            gameObject.SetActive(true);
        }

        public Vector2 GetMomentum()
        {
            return _speed * _direction;
        }

        private void UpdateDirection()
        {
            var destination = _target.position - transform.position;

            if (destination.magnitude >= _stopFollowingDistance)
            {
                _direction = destination.normalized;
            }
            else
            {
                _wasCloseToTarget = true;
            }
        }

        private void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, (-1) * _direction);
        }        
    }
}