using UnityEngine;

namespace GameScripts.RunTime.Magic
{
    public class MagicDefine
    {

    }


    public enum GetType
    {
        [InspectorName("随机")] Random,
        [InspectorName("顺序")] List
    }


    public enum MagicType
    {
        [InspectorName("普通法术")] NormalSpell = 1
    }

    public enum PositionType
    {
        [InspectorName("无")] None,
        [InspectorName("攻击者位置")] AttackerPosition,
        [InspectorName("受击者位置")] VictimPosition,
        [InspectorName("攻击者阵法站位")] AttackerLineup,
        [InspectorName("受击者阵法站位")] VictimLineup,
        [InspectorName("战场中心")] BattlefieldCenter,
        [InspectorName("攻击队伍中心")] AttackerTeamCenter,
        [InspectorName("受击队伍中心")] VictimTeamCenter,
        [InspectorName("像机位置")] CameraPosition
    }
    
    public enum ExecutorType
    {
        [InspectorName("攻击者")] Attacker,
        [InspectorName("受击者")] Victim,
        [InspectorName("受击者(全部)")] AllVictims,
        [InspectorName("像机")] Camera,
        [InspectorName("友军(全部)")] Allies,
        [InspectorName("友军(非攻击者)")] AlliesExceptAttacker,
        [InspectorName("友军(存活)")] LivingAllies,
        [InspectorName("敌军(全部)")] Enemies,
        [InspectorName("敌军(非受击者)")] EnemiesExceptVictim
    }


    /// <summary>
    /// 绑定类型
    /// </summary>
    public enum BindType
    {
        [InspectorName("无")] Empty,
        [InspectorName("节点")] Node,
        [InspectorName("部位")] Pos,
    }
    

    /// <summary>
    /// 身体部位
    /// </summary>
    public enum BodyPart
    {
        [InspectorName("头部")] Head,
        [InspectorName("腰部")] Waist,
        [InspectorName("脚部")] Foot
    }

    /// <summary>
    /// 施法层级类型
    /// </summary>
    public enum MagicLayer
    {
        [InspectorName("正常")] normal,
        [InspectorName("遮挡")] Cover,
        [InspectorName("置顶")] Top
    }

    public enum MoveType
    {
        [InspectorName("直线")] Line,
        [InspectorName("圆弧")] Circle,
        [InspectorName("跳跃")] Jump
    }

    
    public enum EffectCountType
    {
        [InspectorName("单个")] Single,
        [InspectorName("所有受击者")] AllVictims
    }


    public enum MoveDirection
    {
        [InspectorName("本地坐标up")] LocalUp,
        [InspectorName("本地坐标right")] LocalRight,
        [InspectorName("本地坐标forward")] LocalForward,
        [InspectorName("世界坐标up")] WorldUp,
        [InspectorName("世界坐标right")] WorldRight,
        [InspectorName("世界坐标forward")] WorldForward
    }


    public enum ExecutorDirection
    {
        [InspectorName("无")] None,
        [InspectorName("绑定人-前")] Forward,
        [InspectorName("绑定人-后")] Backward,
        [InspectorName("绑定人-左")] Left,
        [InspectorName("绑定人-右")] Right,
        [InspectorName("绑定人-上")] Up,
        [InspectorName("绑定人-下")] Down,
        [InspectorName("自定义")] Custom
    }


    #region 指令组
    /// <summary>
    /// 指令组类型
    /// </summary>
    public enum CmdGroupType
    {
        [InspectorName("条件判断")] Condition,
        [InspectorName("重复播放")] Repeat,
    }

    /// <summary>
    /// 指令组条件判断类型
    /// </summary>
    public enum CmdGroupCondition
    {
        [InspectorName("友方")] Ally,
        [InspectorName("没有额外攻击")] Endidx,
        [InspectorName("1024")] Screen1024,
    }

    /// <summary>
    /// 指令组添加方式
    /// </summary>
    public enum CmdGroupAddType
    {
        [InspectorName("插入")] Insert,
        [InspectorName("合并")] Merge,
    }
    #endregion

    /// <summary>
    /// 朝向
    /// </summary>
    public enum FaceType
    {
        [InspectorName("默认方向")] Default,
        [InspectorName("固定位置")] Fixed_pos,
        [InspectorName("匀速旋转")] Lerp_pos,
        [InspectorName("LookAt")] Look_at,
        [InspectorName("随机方向")] Random,
        // [InspectorName("预设点")] Prepare,
    }

    /// <summary>
    /// 位置
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// 当前位置
        /// </summary>
        [InspectorName("当前位置")] Current,
        /// <summary>
        /// 预设
        /// </summary>
        [InspectorName("预设")] Prepare,
        /// <summary>
        /// 自定义
        /// </summary>
        [InspectorName("自定义")] Relative,
    }


    /// <summary>
    /// 特写镜头移动类型
    /// </summary>
    public enum CloseUpMoveType
    {
        [InspectorName("相机->人物")] Cam,
        [InspectorName("人物->相机")] Actor,
    }
}