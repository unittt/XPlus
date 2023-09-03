
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace cfg.SystemModule
{
public sealed partial class RoleType : Luban.BeanBase
{
    public RoleType(ByteBuf _buf) 
    {
        ID = _buf.ReadInt();
        Name = _buf.ReadString();
        Sex = (SystemModule.ESex)_buf.ReadInt();
        Race = (SystemModule.ERace)_buf.ReadInt();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);SchoolList = new System.Collections.Generic.List<SystemModule.ESchoolType>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { SystemModule.ESchoolType _e0;  _e0 = (SystemModule.ESchoolType)_buf.ReadInt(); SchoolList.Add(_e0);}}
        Racedesc = _buf.ReadString();
        NamePath = _buf.ReadString();
    }

    public static RoleType DeserializeRoleType(ByteBuf _buf)
    {
        return new SystemModule.RoleType(_buf);
    }

    /// <summary>
    /// 角色id
    /// </summary>
    public readonly int ID;
    /// <summary>
    /// 角色显示名
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// 性别
    /// </summary>
    public readonly SystemModule.ESex Sex;
    /// <summary>
    /// 种族
    /// </summary>
    public readonly SystemModule.ERace Race;
    /// <summary>
    /// 可加入门派
    /// </summary>
    public readonly System.Collections.Generic.List<SystemModule.ESchoolType> SchoolList;
    /// <summary>
    /// 种族说明
    /// </summary>
    public readonly string Racedesc;
    /// <summary>
    /// 角色显示名路径
    /// </summary>
    public readonly string NamePath;
   
    public const int __ID__ = 1411027939;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "ID:" + ID + ","
        + "Name:" + Name + ","
        + "Sex:" + Sex + ","
        + "Race:" + Race + ","
        + "SchoolList:" + Luban.StringUtil.CollectionToString(SchoolList) + ","
        + "Racedesc:" + Racedesc + ","
        + "NamePath:" + NamePath + ","
        + "}";
    }
}

}
