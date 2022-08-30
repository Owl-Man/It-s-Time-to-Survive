namespace NTC.Global.System
{
    public static class ConvertExt
    {
        public static int ToInt(this bool b) => b ? 1 : 0;
        public static int ToInt(this float b) => (int) b;
        public static bool ToBool(this int i) => i == 1;
        public static bool ToBool(this float i) => i == 1;
    }
}