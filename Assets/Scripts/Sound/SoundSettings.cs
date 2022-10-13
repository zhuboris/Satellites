using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utils;

namespace Sound
{
    public abstract class SoundSettings : MonoBehaviour
    {
        private const int Min = -80;
        private const int Max = 0;

        [SerializeField] private AudioMixer _masterMixer;

        private bool _isMuted;

        protected abstract string SaveKey { get; }
        protected abstract string ExposedParameter { get; }

        protected void Start()
        {
            _isMuted = Saver.Load(key: SaveKey);
            MuteOnLoad();
        }

        public void OnCheckBoxChanged(bool isOn)
        {
            ToggleMute(isMuted: isOn);
            Saver.Save(key: SaveKey, value: _isMuted);
        }

        public void SetCheckBoxState(Toggle checkBox)
        {
            checkBox.isOn = !_isMuted;
        }

        private void ToggleMute(bool isMuted)
        {
            if (isMuted)
            {
                Unmute();
            }
            else
            {
                Mute();
            }

            _isMuted = !isMuted;
        }

        private void MuteOnLoad()
        {
            if (_isMuted)
            {
                Mute();
            }
        }

        private void Unmute()
        {
            _masterMixer.SetFloat(ExposedParameter, Max);
        }

        private void Mute()
        {
            _masterMixer.SetFloat(ExposedParameter, Min);
        }
    }
}