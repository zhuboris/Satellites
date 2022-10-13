using InputSystem;
using System.Collections;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerPhysics : MonoBehaviour
    {
        private const float Epsilon = 0.01f;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxSpeedAtRest;
        [SerializeField] private float _maxSpeedAfterKnock;
        [SerializeField] private float _dampForce;
        [SerializeField] private float _maxSpeedChangeStep;

        private Rigidbody2D _rigidbody;
        private PlayerMovement _input;
        private Vector2 _direction;
        private float _currentMaxSpeed;
        private Coroutine _maxSpeedChangingCoroutine;

        private void Awake()
        {
            _input = new PlayerMovement();
            _input.Enable();

            _rigidbody = GetComponent<Rigidbody2D>();
            _currentMaxSpeed = _maxSpeedAtRest;
        }

        private void FixedUpdate()
        {
            LimitSpeed();
            _direction = GetDirection();

            if (_direction.sqrMagnitude < Epsilon)
            {
                Damp();
                return;
            }

            Move();
        }

        public void AddForce(Vector2 force)
        {
            ChangeMaxSpeed();
            _rigidbody.AddForce(force);
        }                

        private void Damp()
        {
            if (_rigidbody.velocity.magnitude < Epsilon)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            _rigidbody.velocity = _dampForce * Time.fixedDeltaTime * _rigidbody.velocity;
        }

        private void Move()
        {
            _rigidbody.AddForce(_moveSpeed * Time.fixedDeltaTime * _direction);
        }

        private void LimitSpeed()
        {
            if (_rigidbody.velocity.magnitude > _currentMaxSpeed)
            {
                _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _currentMaxSpeed);
            }
        }

        private Vector2 GetDirection()
        {
            if (Game.IsActive)
            {
                return _input.Player.Move.ReadValue<Vector2>();
            }
            else
            {
                return Random.insideUnitCircle;
            }
        }

        private void ChangeMaxSpeed()
        {
            _currentMaxSpeed = _maxSpeedAfterKnock;

            if (_maxSpeedChangingCoroutine is not null)
            {
                StopCoroutine(_maxSpeedChangingCoroutine);
            }

            _maxSpeedChangingCoroutine = StartCoroutine(DecreaseMaxSpeed());
        }

        private IEnumerator DecreaseMaxSpeed()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (_currentMaxSpeed > _maxSpeedAtRest)
            {
                _currentMaxSpeed -= _maxSpeedChangeStep * Time.fixedDeltaTime;
                yield return waitForFixedUpdate;
            }
        }
    }
}