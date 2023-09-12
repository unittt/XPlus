namespace GameScripts.RunTime.Utility.Variable
{
    /// <summary>
    /// float变量
    /// </summary>
    public class VarFloat:Variable<float> 
    {
        protected override bool IsEquals(float oldValue, float newValue)
        {
            return oldValue == newValue;
        }

        public static implicit operator float(VarFloat value)
        {
            return value.Value;
        }
    }
}