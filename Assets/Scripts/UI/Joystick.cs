using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class Joystick : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Vector3 _startPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.position;
        }

        private void OnDisable()
        {
            _rectTransform.position = _startPosition;
        }
    }
}