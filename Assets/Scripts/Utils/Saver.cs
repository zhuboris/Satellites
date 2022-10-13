using UnityEngine;

namespace Utils
{
    public static class Saver
    {
        public static void Save(string key, bool value)
        {
            int data = Convertor.BoolToInt(input: value);
            PlayerPrefs.SetInt(key, data);
        }

        public static bool Load(string key)
        {
            int value = PlayerPrefs.GetInt(key, 0);
            return Convertor.IntToBool(input: value);
        }
    }
}