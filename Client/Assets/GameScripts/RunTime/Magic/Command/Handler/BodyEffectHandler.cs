using System.Collections.Generic;
using GameScripts.RunTime.War;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 绑定身体处理
    /// </summary>
    public class BodyEffectHandler:CmdHandlerBase<BodyEffect>
    {
        protected override void OnFill(BodyEffect commandData)
        {
            //1. 获取执行者
            List<Warrior> warriors = new List<Warrior>();
            
            //2.加载特效
            
            //3.绑定特效
        }
    }
}