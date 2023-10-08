
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
public sealed partial class SchoolSkill : Luban.BeanBase
{
    public SchoolSkill(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Name = _buf.ReadString();
        Icon = _buf.ReadInt();
        Funcdesc = _buf.ReadString();
        Rolecreatedesc = _buf.ReadString();
    }

    public static SchoolSkill DeserializeSchoolSkill(ByteBuf _buf)
    {
        return new SkillModule.SchoolSkill(_buf);
    }

    /// <summary>
    /// 门派技能编号
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 技能名字
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// 技能图标
    /// </summary>
    public readonly int Icon;
    /// <summary>
    /// 功效描述
    /// </summary>
    public readonly string Funcdesc;
    /// <summary>
    /// 创建角色使用的功效描述
    /// </summary>
    public readonly string Rolecreatedesc;
   
    public const int __ID__ = -1380575572;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "icon:" + Icon + ","
        + "funcdesc:" + Funcdesc + ","
        + "rolecreatedesc:" + Rolecreatedesc + ","
        + "}";
    }
}

}