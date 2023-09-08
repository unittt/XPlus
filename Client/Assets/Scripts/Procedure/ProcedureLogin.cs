using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using Protocol.Character;
using Protocol.Player;
using UnityEngine;

/// <summary>
/// 登录流程
/// </summary>
public class ProcedureLogin : ProcedureBase
{
    public override void OnEnter(ProcedureBase lastProcedure)
    {
        base.OnEnter(lastProcedure);
        Main.m_UI.OpenUI<UILogin>();
        
        
        NetManager.Subscribe((int)ActionCmd.cmd,(int)ActionCmd.enterGame, OnEnter);
        NetManager.Subscribe((int)ActionCmd.cmd,(int)ActionCmd.move, OnMove);
        NetManager.Subscribe((int)ActionCmd.cmd,(int)ActionCmd.leaveMap, OnLeave);
        NetManager.Subscribe((int)ActionCmd.cmd,(int)ActionCmd.syncPlayer, OnList);
        
        NetManager.ConnectServer();
        Main.m_Network.ConnectServerSuccessEvent += OnConnectServerSuccessEvent;
      
    }

    private void OnConnectServerSuccessEvent(ProtocolChannelBase arg)
    {
        CharacterProto characterProto = new CharacterProto()
        {
            CharacterId = "136",
            MapId = 1,
            MapPostX = 0,
            MapPostY = 0,
            MapPostZ = 0,
            Orientation = 0,
        };
        NetManager.SendMessage((int)ActionCmd.cmd, (int)ActionCmd.enterGame, characterProto);
        xxxxxx().Forget();
    }

    

    private async UniTaskVoid xxxxxx()
    {
        var step = 1;
        while (true)
        {
            PlayerMoveProto playerMoveProto = new PlayerMoveProto()
            {
                CharacterId = "136",
                X = step,
                Y = step,
                Z = step,
            };

            step++;
            NetManager.SendMessage(2, 4, playerMoveProto);
            await UniTask.Delay(1);
        }

      
    }
    
    private void OnEnter(ProtocolTcpNetworkInfo arg)
    {
        
        // 同步周围玩家请求
        NetManager.SendMessage((int)ActionCmd.cmd, (int)ActionCmd.syncPlayer,
            new SyncMapPlayerProto());
    }
    
    private void OnMove(ProtocolTcpNetworkInfo arg)
    {
        PlayerMoveProto playerMoveProto = arg.GetMessage<PlayerMoveProto>();

        // if (!otherHumans.ContainsKey(playerMoveProto.CharacterId))
        //     return;

        Vector3 targetPos = new Vector3(playerMoveProto.X, playerMoveProto.Y, playerMoveProto.Z);
     Log.Info("用户"+ playerMoveProto.CharacterId +"移动中啊" + targetPos);
    }
    
    private void OnLeave(ProtocolTcpNetworkInfo arg)
    {
        
    }
    

    private void OnList(ProtocolTcpNetworkInfo arg)
    {
        Log.Info("同步List消息");
        var syncMapPlayer = arg.GetMessage<SyncMapPlayerProto>();
        foreach (CharacterProto characterMsg in syncMapPlayer.PlayerCharacterList)
        {
            Log.Info("角色编号是"+characterMsg.CharacterId);
        }
    }
}
