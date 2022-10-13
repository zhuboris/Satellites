namespace Sound
{
    public sealed class MusicSettings : SoundSettings
    {
        protected override string SaveKey => SoundConstants.SaveKeys.Music;
        protected override string ExposedParameter => SoundConstants.ExposedParameters.Music;
    }
}