
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace cfg.SkillModule
{
public sealed partial class SchoolSkillLVArg : Luban.BeanBase
{
    public SchoolSkillLVArg(ByteBuf _buf) 
    {
        LearnLimit = _buf.ReadInt();
        SkillpointLearn = _buf.ReadInt();
        ClientRange = _buf.ReadInt();
        ClientHpResume = _buf.ReadInt();
        ClientMpResume = _buf.ReadInt();
        ClientAuraResume = _buf.ReadInt();
        Desc = _buf.ReadString();
    }

    public static SchoolSkillLVArg DeserializeSchoolSkillLVArg(ByteBuf _buf)
    {
        return new SkillModule.SchoolSkillLVArg(_buf);
    }

    /// <summary>
    /// 技能学习限制
    /// </summary>
    public readonly int LearnLimit;
    /// <summary>
    /// 学习消耗技能点
    /// </summary>
    public readonly int SkillpointLearn;
    /// <summary>
    /// 作用人数，客户端表现使用
    /// </summary>
    public readonly int ClientRange;
    /// <summary>
    /// 使用消耗气血，客户端表现使用
    /// </summary>
    public readonly int ClientHpResume;
    /// <summary>
    /// 使用消耗法术，客户端表现使用
    /// </summary>
    public readonly int ClientMpResume;
    /// <summary>
    /// 使用消耗灵气，客户端表现使用
    /// </summary>
    public readonly int ClientAuraResume;
    /// <summary>
    /// 描述
    /// </summary>
    public readonly string Desc;
   
    public const int __ID__ = 679496000;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "learnLimit:" + LearnLimit + ","
        + "skillpointLearn:" + SkillpointLearn + ","
        + "clientRange:" + ClientRange + ","
        + "clientHpResume:" + ClientHpResume + ","
        + "clientMpResume:" + ClientMpResume + ","
        + "clientAuraResume:" + ClientAuraResume + ","
        + "desc:" + Desc + ","
        + "}";
    }
}

}
