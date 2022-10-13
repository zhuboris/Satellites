using UnityEngine.EventSystems;

namespace UI.CheckBox
{
    public sealed class SfxCheckBox : CheckBox
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            SettingsComponent.OnCheckBoxChanged(isOn: Toggle.isOn);

            if (Toggle.isOn)
            {
                PlaySFX();
            }
        }
    }
}