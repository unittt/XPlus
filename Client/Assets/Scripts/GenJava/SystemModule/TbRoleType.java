
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg.SystemModule;

import luban.*;


public final class TbRoleType {
    private final java.util.HashMap<Integer, cfg.SystemModule.RoleType> _dataMap;
    private final java.util.ArrayList<cfg.SystemModule.RoleType> _dataList;
    
    public TbRoleType(ByteBuf _buf) {
        _dataMap = new java.util.HashMap<Integer, cfg.SystemModule.RoleType>();
        _dataList = new java.util.ArrayList<cfg.SystemModule.RoleType>();
        
        for(int n = _buf.readSize() ; n > 0 ; --n) {
            cfg.SystemModule.RoleType _v;
            _v = cfg.SystemModule.RoleType.deserialize(_buf);
            _dataList.add(_v);
            _dataMap.put(_v.ID, _v);
        }
    }

    public java.util.HashMap<Integer, cfg.SystemModule.RoleType> getDataMap() { return _dataMap; }
    public java.util.ArrayList<cfg.SystemModule.RoleType> getDataList() { return _dataList; }

    public cfg.SystemModule.RoleType get(int key) { return _dataMap.get(key); }

}