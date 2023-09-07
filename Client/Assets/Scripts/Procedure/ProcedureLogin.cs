using HT.Framework;
using Protocol.Character;

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
        
        CharacterProto characterProto = new CharacterProto()
        {
            CharacterId = "13646",
            MapId = 1,
            MapPostX = 0,
            MapPostY = 0,
            MapPostZ = 0,
            Orientation = 0,
        };
        NetManager.SendMessage((int)ActionCmd.cmd, (int)ActionCmd.enterGame, characterProto);
    }
    
    private void OnEnter(ProtocolTcpNetworkInfo arg)
    {
        
    }
    
    private void OnMove(ProtocolTcpNetworkInfo arg)
    {
       
    }
    
    private void OnLeave(ProtocolTcpNetworkInfo arg)
    {
        
    }
    

    private void OnList(ProtocolTcpNetworkInfo arg)
    {
        
    }
}
