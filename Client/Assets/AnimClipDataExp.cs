using DG.Tweening;
using HT.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GameScripts.RunTime.DataUser;
using UnityEngine;

public  class AnimClipDataExp : MonoBehaviour
{

    public TextAsset luaText;
    
    
    public static void Main()
    {
        string input = "[-1]={attack1={frame=21,length=0.70000004768372,},attack2={frame=24,length=0.80000007152557,},...}";
        string pattern = "(\\w+)=\\{frame=(.*?),length=(.*?)[,}]";

        foreach (Match match in Regex.Matches(input, pattern))
        {
            // Console.WriteLine($"Attack: {match.Groups[0].Value}, Frame: {match.Groups[1].Value}, Length: {match.Groups[2].Value}");
        }
    }
    
    public  Dictionary<int, List<ActionData>> ParseLuaData(string luaData)
    {
        var data = new Dictionary<int, List<ActionData>>();

        // 正则表达式以匹配整个数据块
        var entryRegex = new Regex(@"\[(\d+)\]={([^}]+)}");
        var matches = entryRegex.Matches(luaData);

        foreach (Match match in matches)
        {
            int id = int.Parse(match.Groups[1].Value);
            var actionsString = match.Groups[2].Value;

            Log.Info(actionsString);
            var actions = new List<ActionData>();
            // 更新正则表达式以匹配单个动作数据
            // var actionRegex = new Regex(@"(\w+)={frame=(\d+),\s*length=(\d+\.\d+),");
            // var actionRegex = new Regex(@"(\w+)={frame=(\d+),\s*length=([\d.]+)}");


            var actionRegex = new Regex("(\\w+)=\\{frame=(.*?),length=(.*?),");
            var actionMatches = actionRegex.Matches(actionsString);

            foreach (Match actionMatch in actionMatches)
            {
                var actionData = new ActionData
                {
                    key = actionMatch.Groups[1].Value,
                    frame = int.Parse(actionMatch.Groups[2].Value),
                    length = float.Parse(actionMatch.Groups[3].Value)
                };
                actions.Add(actionData);
            }

            data.Add(id, actions);
        }

        return data;
    }


    [Button("导出")]
    public  void xxxxxxx()
    {
        string luaData = luaText.text;

        var parsedData = ParseLuaData(luaData);

        // 打印解析后的数据以验证
        foreach (var entry in parsedData)
        {
            Log.Info($"ID: {entry.Key}");
            foreach (var action in entry.Value)
            {
                Log.Info($"  Action: {action.key}, Frame: {action.frame}, Length: {action.length}");
            }
        }
    }
    
}
