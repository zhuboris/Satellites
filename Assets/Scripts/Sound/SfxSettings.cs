namespace Sound
{
    public sealed class SfxSettings : SoundSettings
    {
        protected override string SaveKey => SoundConstants.SaveKeys.Sfx;
        protected override string ExposedParameter => SoundConstants.ExposedParameters.Sfx;
    }
}