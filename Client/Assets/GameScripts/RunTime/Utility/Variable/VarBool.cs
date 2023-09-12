namespace GameScripts.RunTime.Utility.Variable
{
    public class VarBool:Variable<bool> 
    {
        protected override bool IsEquals(bool oldValue, bool newValue)
        {
            return oldValue == newValue;
        }


        public static implicit operator bool(VarBool value)
        {
            return value.Value;
        }
    }
}