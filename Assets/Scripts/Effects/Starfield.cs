using UnityEngine;
using UnityEngine.Rendering;

namespace Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(ParticleSystemRenderer))]
    public class Starfield : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _followedTarget;
        [SerializeField] private int _maxStars = 100;
        [SerializeField] private float _minSarSize = 0.1f;
        [SerializeField] private float _maxSarSize = 0.5f;
        [SerializeField] private float _fieldWidth = 20f;
        [SerializeField] private float _fieldHeight = 25f;
        [SerializeField] private Color _color1;
        [SerializeField] private Color _color2;
        [SerializeField] private float _speed = 0.5f;
        [SerializeField] private float _movingDistanceDelta = 0.2f;

        private float _xOffset;
        private float _yOffset;

        private ParticleSystem _particles;
        private ParticleSystem.Particle[] _stars;

        private void Awake()
        { 
            _xOffset = _fieldWidth * 0.5f;
            _yOffset = _fieldHeight * 0.5f;

            InitParticleSystem();
        }

        private void Update()
        {
            MoveStars();
        }

        private void InitParticleSystem()
        {
            var renderer = GetComponent<ParticleSystemRenderer>();
            renderer.material.renderQueue = (int)RenderQueue.Background;

            _stars = new ParticleSystem.Particle[_maxStars];
            _particles = GetComponent<ParticleSystem>();

            for (int i = 0; i < _stars.Length; i++)
            {
                InitStar(i);
            }

            _particles.SetParticles(_stars, _stars.Length);
        }

        private void InitStar(int index)
        {
            float randomSize = Random.Range(_minSarSize, _maxSarSize);

            _stars[index].position = GetRandomPointInRectangle(_fieldWidth, _fieldHeight) + transform.position;
            _stars[index].startSize = randomSize;
            _stars[index].startColor = GetRandomBetweenTwoColors(_color1, _color2);
        }

        private Vector3 GetRandomPointInRectangle(float width, float height)
        {
            float x = Random.Range(0, width);
            float y = Random.Range(0, height);
            return new(x - _xOffset, y - _yOffset, 0f);
        }

        private Color GetRandomBetweenTwoColors(Color first, Color second)
        {
            const float MinLerpParameter = 0; 
            const float MaxLerpParameter = 1;

            float parameter = Random.Range(MinLerpParameter, MaxLerpParameter);
            return Color.Lerp(first, second, parameter);
        }

        private void MoveStars()
        {
            var targetVelocity = (Vector3)_followedTarget.velocity;
            transform.position += (-1) * _speed * Time.deltaTime * targetVelocity;

            for (int i = 0; i < _stars.Length; i++)
            {
                MoveHorisontally(i, targetVelocity.x);
                MoveVertically(i, targetVelocity.y);
            }

            _particles.SetParticles(_stars, _stars.Length);
        }

        private void MoveHorisontally(int starIndex, float xVelocity)
        {
            var direction = Mathf.Sign(xVelocity);
            var position = _stars[starIndex].position;
            float systemXDistanceToTarget = _followedTarget.position.x - transform.position.x;

            if (position.x * direction < systemXDistanceToTarget * direction - _xOffset)
            {
                position.x += _fieldWidth * direction;
                UpdateStarPosition(starIndex, position);
            }
        }

        private void MoveVertically(int starIndex, float yVelocity)
        {
            var direction = Mathf.Sign(yVelocity);
            var position = _stars[starIndex].position;
            float systemYDistanceToTarget = _followedTarget.position.y - transform.position.y;

            if (position.y * direction < systemYDistanceToTarget * direction - _yOffset)
            {
                position.y += _fieldHeight * direction;
                UpdateStarPosition(starIndex, position);
            }
        }

        private void UpdateStarPosition(int starIndex, Vector3 position)
        {
            _stars[starIndex].position = position + GetShift();
        }

        private Vector3 GetShift()
        {
            return Random.insideUnitCircle * _movingDistanceDelta;
        }
    }
}