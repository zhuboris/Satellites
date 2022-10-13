using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SourceIgnoringPause : MonoBehaviour
    {
        private void Awake()
        {
            var source = GetComponent<AudioSource>();
            source.ignoreListenerPause = true;
        }
    }
}