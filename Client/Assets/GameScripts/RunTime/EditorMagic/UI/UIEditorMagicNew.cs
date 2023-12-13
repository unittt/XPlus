using System.IO;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Utility;
using Google.Protobuf;
using HT.Framework;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    
    [UIResource("UIEditorMagicNew")]
    public sealed class UIEditorMagicNew:UILogicResident
    {
        private InputField _inputField;
        
        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            _inputField = variableArray.Get<InputField>("inputField");
            variableArray.Get<Button>("confirmBtn").onClick.AddListener(OnClickConfirm);
            variableArray.Get<Button>("closeBtn").onClick.AddListener(Close);
        }

        public override void OnOpen(params object[] args)
        {
            base.OnOpen(args);
            _inputField.text = "";
        }

        private void OnClickConfirm()
        {
            if (string.IsNullOrEmpty( _inputField.text))
            {
                return;
            }
            
            var fileName = $"{_inputField.text}.bytes"; // 指定文件名
            var path = "Assets/GameRes/MagicFile";
            path = Path.Combine(path, fileName);

            var magicData = new MagicData();
            var faceTo = new FaceTo();
            faceTo.pos = new ComplexPosition();
            faceTo.pos.Depth = 100;
            magicData.Commands.Add(faceTo);

            var bytes = magicData.SerializeCommands();
            File.WriteAllBytes(path,bytes);
            
            // 刷新Asset数据库，以便Unity编辑器能够检测到新文件
            AssetDatabase.Refresh();
            Debug.Log("MagicData保存成功");
            
            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            var newData = MagicData.Deserialize(textAsset.bytes);
            Log.Info(newData.Commands.Count.ToString());
            
        }
        
        // var newData = JsonToolkit.StringToJson<MagicData>(json);
        // JsonConvert.SerializeObject(magicData);
    }
}