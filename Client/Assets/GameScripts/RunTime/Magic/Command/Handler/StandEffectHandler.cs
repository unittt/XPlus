using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameScripts.RunTime.War;
using HT.Framework;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 定点特效处理
    /// </summary>
    public class StandEffectHandler:CmdHandlerBase<StandEffect>
    {
        private List<Warrior> _warriors = new();
        
        protected override void OnFill(StandEffect commandData)
        {
            GetExecutors(commandData.executor,_warriors);

            if (_warriors.Count == 0)
            {
                return;
            }
            
            var atkObj = MagicUnit.AtkObj;
            var vicObjs = MagicUnit.VicObjs;

            for (var i = 0; i < _warriors.Count; i++)
            {
                NewEffect(commandData,atkObj,vicObjs[i], _warriors[i]).Forget();
            }
            
        }

        private async UniTaskVoid NewEffect(StandEffect commandData,Warrior atkObj, Warrior vicObj , Warrior warrior)
        {
            var magicEffect = await Main.m_Entity.CreateEntity<MagicEffectEntityLogic>();
            magicEffect.Fill(commandData.effect.Path, 0, commandData.effect.Cached);

            var pos = MagicTools.CalcPos(commandData.effect_pos, atkObj, vicObj, false);
            magicEffect.SetLocalPos(pos);
            
            if (commandData.effect_dir_type != ExecutorDirection.None)
            {
            
                if (magicEffect.Parent == warrior.Parent)
                {
                   var localEulerAngles = MagicTools.GetExecutorLocalAngle(warrior, commandData.effect_dir_type);
                   magicEffect.SetLocalEulerAngles(localEulerAngles);
                }
                else
                {
                    var dirPos = MagicTools.GetExecutorDirPos(warrior, commandData.effect_dir_type, pos);
                    dirPos = MagicTools.CalcPos(commandData.effect_pos, atkObj, vicObj, false);
                    magicEffect.Entity.transform.LookAt(dirPos, warrior.Entity.transform.up);
                }
            }
        }
    }
}