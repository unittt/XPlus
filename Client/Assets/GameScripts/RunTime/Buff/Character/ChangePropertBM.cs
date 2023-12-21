namespace GameScripts.RunTime.Buff
{
    public class ChangePropertBM : BaseBuffModule
    {
        public Property property;
        
        public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
        {
            var character = buffInfo.target.GetComponent<Character>();
            if (character)
            {
                character.property.hp += property.hp;
                character.property.atk += property.atk;
            }
        }
    }
}