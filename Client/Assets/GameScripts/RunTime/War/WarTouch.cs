namespace GameScripts.RunTime.War
{
    public static class WarTouch
    {

        public static bool IsLock { get; private set; }

        public static void SetLock(bool value)
        {
            IsLock = value;
        }
    }
}