using System;
using HT.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using GameScripts.RunTime.DataUser;
using UnityEngine;

public  class AnimClipDataExp : MonoBehaviour
{

    public TextAsset luaText;
    

    [Button("导出")]
    public  void xxxxxxx()
    {
        string luaData = luaText.text;
        
        var parsedData = ParseLuaData(luaData);
        
        // 打印解析后的数据以验证
        foreach (var entry in parsedData)
        {
            foreach (var action in entry.Value)
            {
                Log.Info($" ID: {entry.Key} Action: {action.key}, Frame: {action.frame}, Length: {action.length}");
            }
        }
    }


    private Dictionary<int, List<ActionData>> ParseLuaData(string content)
    {
        
        var data = new Dictionary<int, List<ActionData>>();
        
        using (StringReader reader = new StringReader(content))
        {
            string line;
            var id = 0;
            
            while ((line = reader.ReadLine()) != null)
            {
                // 正则表达式以匹配整个数据块
                var entryRegex = new Regex(@"\[(\d+)\]");
                var matches = entryRegex.Matches(line);
                if (matches.Count > 0)
                {
                    id = int.Parse(matches[0].Groups[1].Value);
                    data.Add(id, new List<ActionData>());
                }

                if (id <= 0) continue;
                var actionRegex = new Regex(@"(\w+)={frame=(\d+),\s*length=(\d+\.\d+),}");
                var actionMatches = actionRegex.Matches(line);

                if (actionMatches.Count <= 0) continue;

                var actionDatas = data[id];
                
                foreach (Match actionMatch in actionMatches)
                {
                    var actionData = new ActionData
                    {
                        key = actionMatch.Groups[1].Value,
                        frame = int.Parse(actionMatch.Groups[2].Value),
                        length = (float)Math.Round(float.Parse(actionMatch.Groups[3].Value), 2)
                    };
                    actionDatas.Add(actionData);
                }
            }
        }

        return data;
    }
    
}
