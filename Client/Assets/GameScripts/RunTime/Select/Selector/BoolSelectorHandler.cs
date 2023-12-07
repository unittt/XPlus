using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameScripts.RunTime.Select.Selector
{
    // public class BoolSelectorHandler:SelectorHandler
    //
    // {
    //     private List<string> _list;
    //     private Dictionary<string, BodyPart> xxxa;
    //
    //     public BoolSelectorHandler()
    //     {
    //     
    //         _list = new List<string>();
    //         foreach (var value in Enum.GetValues(typeof(BodyPart)))
    //         {
    //             var fieldInfo = typeof(BodyPart).GetField(value.ToString());
    //             var xx = (BodyPart)fieldInfo.GetValue(null);
    //             var attributes = (InspectorNameAttribute[])fieldInfo.GetCustomAttributes(typeof(InspectorNameAttribute), false);
    //
    //             var inspectorName = attributes.Length > 0 ? attributes[0].displayName : value.ToString();
    //             _list.Add(inspectorName);
    //             xxxa.Add(inspectorName, xx);
    //         }
    //     }
        
        // public override List<string> GetElementCollection()
        // {
        //     return _list;
        // }

        // public override void OnSelect(string value)
        // {
        //     if (xxxa.TryGetValue(value, out var xbbbbb))
        //     {
        //         object obj = null;
        //         var fieldInfo = typeof(BodyPart).GetField(value.ToString());
        //        fieldInfo.SetValue(obj ,xbbbbb);
        //     }
        // }
        
        
        //1.枚举的选择器
        
        //2.路径的选择器
        //人物模型,音效选择器,特效选择器,材质球选择器,技能实体选择器,动作选择器
        
        
        // public static T GetEnumValueByDisplayName<T>(string displayName) where T : Enum
        // {
        //     foreach (var field in typeof(T).GetFields())
        //     {
        //         if (Attribute.GetCustomAttribute(field, typeof(InspectorNameAttribute)) is InspectorNameAttribute attribute && attribute.displayName == displayName)
        //         {
        //             return (T)field.GetValue(null);
        //         }
        //     }
        //
        //     throw new ArgumentException($"No enum value with display name '{displayName}' found in {typeof(T)}");
        // }
    // }
}