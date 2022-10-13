using UnityEngine;
using UnityEngine.UI;

namespace UI.CheckBox
{
    public class SwitchCheckmarkImage : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        public void ChangeSprite(bool isOn)
        {
            _image.sprite = isOn ? _spriteOn : _spriteOff;
        }
    }
}