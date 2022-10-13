using Gameplay;
using Gameplay.Player;
using UnityEngine;

namespace UI
{
    public class UIDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject _title;
        [SerializeField] private GameObject _playButton;
        [SerializeField] private GameObject _joystick;

        private void OnEnable()
        {
            Game.Started += OnNewGameStarted;
            PlayerHealth.Died += OnLose;
        }

        private void OnDisable()
        {
            Game.Started -= OnNewGameStarted;
            PlayerHealth.Died -= OnLose;
        }

        private void OnNewGameStarted()
        {
            _title.SetActive(false);
            _playButton.SetActive(false);
            _joystick.SetActive(true);
        }

        private void OnLose()
        {
            _title.SetActive(true);
            _playButton.SetActive(true);
            _joystick.SetActive(false);
        }
    }
}