using System.Collections.Generic;
using GameScripts.RunTime.Battle.Character;
using GameScripts.RunTime.Battle.Report.Data;

namespace GameScripts.RunTime.Battle.Skill
{
    /// <summary>
    /// 游戏中管理和执行具体的战斗技能
    /// </summary>
    public class BatSkill
    {
        /// <summary>
        /// 技能动画是否完成
        /// </summary>
        public bool isAnimFinished { get; private set; }
        /// <summary>
        /// 技能是否已被销毁
        /// </summary>
        public bool isDestroied { get; private set; }
        /// <summary>
        /// 执行技能的角色
        /// </summary>
        public BatCharacter host { get; private set; }
        public BatRoundSkillData data { get; private set; }
        /// <summary>
        /// 技能目标的列表
        /// </summary>
        private List<BatSkillTarget> mTargets = null;
        /// <summary>
        /// 负责技能的视觉表现
        /// </summary>
        private BatSkillPerformance mPerform = null;
      
        // private SkillPerformTemplate mPerformTpl = null;
        private bool mIsReadyStart = false;
        private bool mIsStarted = false;
        private float mSkillStartDelay = 0f;
        
        public BatSkill(BatCharacter host, BatRoundSkillData data)
        {
            this.host = host;
            this.data = data;

            // if (data.skillTpl != null && data.doPerform && data.skillTpl.notNeedShow == 0)
            // {
            //     if (data.skillTpl.Id == BatSkillID.CATCH || data.skillTpl.Id == BatSkillID.ESCAPE)
            //     {
            //         mPerform = new BatSkillPerformance(this, null);
            //     }
            //     else
            //     {
            //         if (data.skillTpl.Id == BatSkillID.USE_ITEM || data.skillTpl.Id == BatSkillID.SUMMON)
            //         {
            //             mPerformTpl = SkillPerformTemplateDB.Instance.getTemplateByComposeId(host.displayModelId + BatSkillID.NORMAL_ATTACK);
            //         }
            //         else
            //         {
            //             if (data.skillEffects.Count > 0)
            //             {
            //                 int len = data.skillEffects.Count;
            //                 for (int i = len - 1; i >= 0; i--)
            //                 {
            //                     mPerformTpl = SkillPerformTemplateDB.Instance.getTemplateByComposeIdAndEffectId(host.displayModelId + data.skillTpl.Id, data.skillEffects[i]);
            //                     if (mPerformTpl != null)
            //                     {
            //                         break;
            //                     }
            //                 }
            //                 if (mPerformTpl == null)
            //                 {
            //                     mPerformTpl = SkillPerformTemplateDB.Instance.getTemplateByComposeId(host.displayModelId + data.skillTpl.Id);
            //                 }
            //             }
            //             else
            //             {
            //                 mPerformTpl = SkillPerformTemplateDB.Instance.getTemplateByComposeId(host.displayModelId + data.skillTpl.Id);
            //             }
            //         }
            //
            //         if (mPerformTpl != null)
            //         {
            //             mPerform = new BatSkillPerformance(this, mPerformTpl);
            //         }
            //     }
            // }

            // if (data.results != null)
            // {
            //     mTargets = new List<BatSkillTarget>();
            //     int len = data.results.Count;
            //     for (int i = 0; i < len; i++)
            //     {
            //         BatSkillTarget target = new BatSkillTarget(data.results[i]);
            //         mTargets.Add(target);
            //     }
            // }
        }
    }
}