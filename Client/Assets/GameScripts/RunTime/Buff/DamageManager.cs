using UnityEngine;

namespace GameScripts.RunTime.Buff
{
    public class DamageManager : MonoBehaviour
    {
        
        /// <summary>
        /// 处理伤害
        /// </summary>
        /// <param name="damageInfo"></param>
        public void SubmitDamage(DamageInfo damageInfo)
        {
            BuffHandler creatorBuffHandler = damageInfo.creator?.GetComponent<BuffHandler>();
            BuffHandler targetBuffHandler = damageInfo.target?.GetComponent<BuffHandler>();

            if (creatorBuffHandler)
            {
                foreach (var buffInfo in creatorBuffHandler.buffList)
                {
                    buffInfo.buffData.OnHit.Apply(buffInfo);
                }
            }
            
            if (targetBuffHandler)
            {
                foreach (var buffInfo in targetBuffHandler.buffList)
                {
                    buffInfo.buffData.OnBehurt.Apply(buffInfo);
                }

                var character = targetBuffHandler.GetComponent<Character>();
                if (character.IsCanBekill(damageInfo))
                {
                    foreach (var buffInfo in targetBuffHandler.buffList)
                    {
                        buffInfo.buffData.OnBeKill.Apply(buffInfo, damageInfo);
                    }

                    if (character.IsCanBekill(damageInfo))
                    {
                        if (creatorBuffHandler)
                        {
                            foreach (var buffInfo in creatorBuffHandler.buffList)
                            {
                                buffInfo.buffData.OnKill.Apply(buffInfo, damageInfo);
                            }
                        }
                    }
                }
            }
        }
    }
}