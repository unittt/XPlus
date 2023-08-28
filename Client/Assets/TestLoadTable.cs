using HT.Framework;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Luban;
using UnityEngine;

public class TestLoadTable : HTBehaviour
{
    //启用自动化
    protected override bool IsAutomate => true;
    
    // Start is called before the first frame update
    void Start()
    {
        
        var tables = new cfg.Tables(LoadByteBuf);
        var item = tables.TbItem.DataList[1];
        UnityEngine.Debug.LogFormat("item[1]:{0}", item);
        Debug.LogFormat("bag init capacity:{0}", tables.TbItem.DataList.Capacity);

        var refv = tables.TbTestRef.DataList[0].X1_Ref;
        Debug.LogFormat("refv:{0}", refv);

        UnityEngine.Debug.Log("== load succ==");
    }

    private static ByteBuf LoadByteBuf(string file)
    {
        return new ByteBuf(File.ReadAllBytes($"{Application.dataPath}/../../GenerateDatas/bytes/{file}.bytes"));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
