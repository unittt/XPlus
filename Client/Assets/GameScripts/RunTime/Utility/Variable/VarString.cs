namespace GameScripts.RunTime.Utility.Variable
{
    public class VarString:Variable<string> 
    {
        protected override bool IsEquals(string oldValue, string newValue)
        {
            return oldValue == newValue;
        }

        public static implicit operator string(VarString value)
        {
            return value.Value;
        }
    }
}