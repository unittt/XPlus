
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg.SystemModule;

import luban.*;


public final class TbSchool {
    private final java.util.HashMap<Integer, cfg.SystemModule.School> _dataMap;
    private final java.util.ArrayList<cfg.SystemModule.School> _dataList;
    
    public TbSchool(ByteBuf _buf) {
        _dataMap = new java.util.HashMap<Integer, cfg.SystemModule.School>();
        _dataList = new java.util.ArrayList<cfg.SystemModule.School>();
        
        for(int n = _buf.readSize() ; n > 0 ; --n) {
            cfg.SystemModule.School _v;
            _v = cfg.SystemModule.School.deserialize(_buf);
            _dataList.add(_v);
            _dataMap.put(_v.SchoolType, _v);
        }
    }

    public java.util.HashMap<Integer, cfg.SystemModule.School> getDataMap() { return _dataMap; }
    public java.util.ArrayList<cfg.SystemModule.School> getDataList() { return _dataList; }

    public cfg.SystemModule.School get(int key) { return _dataMap.get(key); }

}
