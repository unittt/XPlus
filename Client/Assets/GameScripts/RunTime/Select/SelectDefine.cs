using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameScripts.RunTime.Select
{
    [SelectEnumAttribute]
    public enum GetType
    {
        [InspectorName("随机")]
        Random,
        [InspectorName("顺序")]
        List
    }

    [SelectEnumAttribute]
    public enum MagicType
    {
        [InspectorName("普通法术")]
        NormalSpell = 1
    }
    
    [SelectEnumAttribute]
    public enum PositionType
    {
        [InspectorName("无")]
        None,
        [InspectorName("攻击者位置")]
        AttackerPosition,
        [InspectorName("受击者位置")]
        VictimPosition,
        [InspectorName("攻击者阵法站位")]
        AttackerLineup,
        [InspectorName("受击者阵法站位")]
        VictimLineup,
        [InspectorName("战场中心")]
        BattlefieldCenter,
        [InspectorName("攻击队伍中心")]
        AttackerTeamCenter,
        [InspectorName("受击队伍中心")]
        VictimTeamCenter,
        [InspectorName("像机位置")]
        CameraPosition
    }
    
    [SelectEnumAttribute]
    public enum BoolType
    {
        
        [InspectorName("是")]
        TRUE,
        [InspectorName("否")]
        FALSE
    }

    [SelectEnumAttribute]
    public enum ExecutorType
    {
        [InspectorName("攻击者")]
        Attacker,
        [InspectorName("受击者")]
        Victim,
        [InspectorName("受击者(全部)")]
        AllVictims,
        [InspectorName("像机")]
        Camera,
        [InspectorName("友军(全部)")]
        Allies,
        [InspectorName("友军(非攻击者)")]
        AlliesExceptAttacker,
        [InspectorName("友军(存活)")]
        LivingAllies,
        [InspectorName("敌军(全部)")]
        Enemies,
        [InspectorName("敌军(非受击者)")]
        EnemiesExceptVictim
    }

    [SelectEnumAttribute]
    public enum BodyPart
    {
        [InspectorName("头部")]
        Head,
        [InspectorName("腰部")]
        Waist,
        [InspectorName("脚部")]
        Foot
    }

    [SelectEnumAttribute]
    public enum MoveType
    {
        [InspectorName("直线")]
        Line,
        [InspectorName("圆弧")]
        Circle,
        [InspectorName("跳跃")]
        Jump
    }

    [SelectEnumAttribute]
    public enum EffectCountType
    {
        [InspectorName("单个")]
        Single,
        [InspectorName("所有受击者")]
        AllVictims
    }

    [SelectEnumAttribute]
    public enum MoveDirection
    {
        [InspectorName("本地坐标up")]
        LocalUp,
        [InspectorName("本地坐标right")]
        LocalRight,
        [InspectorName("本地坐标forward")]
        LocalForward,
        [InspectorName("世界坐标up")]
        WorldUp,
        [InspectorName("世界坐标right")]
        WorldRight,
        [InspectorName("世界坐标forward")]
        WorldForward
    }

    [SelectEnumAttribute]
    public enum ExecutorDirection
    {
        [InspectorName("无")]
        None,
        [InspectorName("绑定人-前")]
        Forward,
        [InspectorName("绑定人-后")]
        Backward,
        [InspectorName("绑定人-左")]
        Left,
        [InspectorName("绑定人-右")]
        Right,
        [InspectorName("绑定人-上")]
        Up,
        [InspectorName("绑定人-下")]
        Down,
        [InspectorName("自定义")]
        Custom
    }


    public class XXXXX
    {
        public static List<Type> GetAllEnumTypesWithEnumAttribute()
        {
            var enumTypesWithAttribute = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsEnum && Attribute.IsDefined(type, typeof(SelectEnumAttribute)))
                    {
                        enumTypesWithAttribute.Add(type);
                    }
                }
            }

            return enumTypesWithAttribute;
        }
        
        
        public static IEnumerable<Enum> GetEnumValues(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的类型必须是枚举类型");
            }

            return Enum.GetValues(enumType).OfType<Enum>();
        }


        public static void Test()
        {
            var selectEnums = GetAllEnumTypesWithEnumAttribute();

            foreach (var selectEnum in selectEnums)
            {
                Type enumType = typeof(ExecutorDirection); // 假设 MagicType 是一个枚举类型
                var enumValues = GetEnumValues(enumType);

                foreach (var value in enumValues)
                {
                    Console.WriteLine(value); // 打印出 MagicType 枚举的所有值
                }
            }
        }
    }
    
    
}