using Gameplay.Player;
using System;
using UnityEngine;

namespace Gameplay
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject _playButton;

        public static bool IsActive { get; private set; } = false;

        public static event Action Started;

        private void OnEnable()
        {
            PlayerHealth.Died += OnLose;
        }

        private void OnDisable()
        {
            PlayerHealth.Died -= OnLose;
        }

        private void Start()
        {
            _playButton.SetActive(true);
        }

        public void OnPlayButtonClick()
        {
            Started?.Invoke();
            IsActive = true;
        }

        private void OnLose()
        {
            IsActive = false;
            _playButton.SetActive(true);
        }
    }
}