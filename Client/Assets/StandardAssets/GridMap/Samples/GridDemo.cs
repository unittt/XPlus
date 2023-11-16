using GridMap;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class GridDemo : MonoBehaviour
{

    [FormerlySerializedAs("GridMapManager")] public MapManager mapManager;
    public TextAsset DataAsset;
    
    void Start()
    {
        // var mapData = MapData.Deserialize(DataAsset.bytes);
        // // mapManager.BlockTextureFunc = GetGridTexture;
        // mapManager.SetMapData(mapData);
    }
}
