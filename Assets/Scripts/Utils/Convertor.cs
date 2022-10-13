namespace Utils
{
    public static class Convertor
    {
        public static bool IntToBool(int input)
        {
            return input switch
            {
                0 => false,
                1 => true,
                _ => throw new System.ArgumentException("Integer is not valid"),
            };
        }

        public static int BoolToInt(bool input)
        {
            return input ? 1 : 0;
        }
    }
}