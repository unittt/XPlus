
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg.SystemModule;

import luban.*;


public final class RoleType extends AbstractBean {
    public RoleType(ByteBuf _buf) { 
        ID = _buf.readInt();
        Name = _buf.readString();
        Sex = _buf.readInt();
        Race = _buf.readInt();
        {int n = Math.min(_buf.readSize(), _buf.size());SchoolList = new java.util.ArrayList<Integer>(n);for(int i = 0 ; i < n ; i++) { Integer _e;  _e = _buf.readInt(); SchoolList.add(_e);}}
        Racedesc = _buf.readString();
        NamePath = _buf.readString();
    }

    public static RoleType deserialize(ByteBuf _buf) {
            return new cfg.SystemModule.RoleType(_buf);
    }

    /**
     * 角色id
     */
    public final int ID;
    /**
     * 角色显示名
     */
    public final String Name;
    /**
     * 性别
     */
    public final int Sex;
    /**
     * 种族
     */
    public final int Race;
    /**
     * 可加入门派
     */
    public final java.util.ArrayList<Integer> SchoolList;
    /**
     * 种族说明
     */
    public final String Racedesc;
    /**
     * 角色显示名路径
     */
    public final String NamePath;

    public static final int __ID__ = 1411027939;
    
    @Override
    public int getTypeId() { return __ID__; }

    @Override
    public String toString() {
        return "{ "
        + "(format_field_name __code_style field.name):" + ID + ","
        + "(format_field_name __code_style field.name):" + Name + ","
        + "(format_field_name __code_style field.name):" + Sex + ","
        + "(format_field_name __code_style field.name):" + Race + ","
        + "(format_field_name __code_style field.name):" + SchoolList + ","
        + "(format_field_name __code_style field.name):" + Racedesc + ","
        + "(format_field_name __code_style field.name):" + NamePath + ","
        + "}";
    }
}

