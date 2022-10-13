using UnityEngine;

namespace Utils
{
    public static class FpsSetter
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Set()
        {
            Application.targetFrameRate = 60;
        }
    }
}