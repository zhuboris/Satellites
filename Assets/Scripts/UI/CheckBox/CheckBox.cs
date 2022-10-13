using Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UI.CheckBox
{
    [RequireComponent(typeof(Toggle))]
    [RequireComponent(typeof(AudioSource))]
    public class CheckBox : MonoBehaviour, IPointerClickHandler
    {   
        [SerializeField] protected SoundSettings SettingsComponent;

        private AudioSource _audioSource;

        protected Toggle Toggle;
        
        private void Awake()
        {
            Toggle = GetComponent<Toggle>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            SettingsComponent.SetCheckBoxState(checkBox: Toggle);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            SettingsComponent.OnCheckBoxChanged(isOn: Toggle.isOn);
            PlaySFX();
        }

        public void PlaySFX()
        {
            _audioSource.Play();
        }
    }
}