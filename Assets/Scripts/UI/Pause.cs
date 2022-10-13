using Gameplay;
using UnityEngine;

namespace UI
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject _pausedMenu;
        [SerializeField] private GameObject _title;

        private bool _isPaused = false;

        private void Start()
        {
            _pausedMenu.SetActive(_isPaused);
        }

        private void OnApplicationFocus()
        {
            if ((_isPaused == false) && Game.IsActive)
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            if (_isPaused)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }

            _isPaused = !_isPaused;
            _pausedMenu.SetActive(_isPaused);
            _title.SetActive(IsTitleActive());
        }

        private void Activate()
        {
            Time.timeScale = 0f;
        }

        private void Deactivate()
        {
            Time.timeScale = 1f;
        }

        private bool IsTitleActive()
        {
            return (_isPaused || Game.IsActive) == false;
        }
    }
}