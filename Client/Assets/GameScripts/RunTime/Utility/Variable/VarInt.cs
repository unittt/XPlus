namespace GameScripts.RunTime.Utility.Variable
{
    /// <summary>
    /// int变量
    /// </summary>
    public class VarInt : Variable<int> 
    {
        protected override bool IsEquals(int oldValue, int newValue)
        {
            return oldValue == newValue;
        }
        
        public static implicit operator int(VarInt value)
        {
            return value.Value;
        }
    }
}