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
        var mapData = MapData.Deserialize(DataAsset.bytes);
        mapManager.GridTextFunc = GetGridTexture;
        mapManager.SetMapData(mapData);
    }
    
    private Texture GetGridTexture(MapData mapData, int x, int y)
    {
        var textureName = GridMapConfig.Instance.TextureNameRule;
        textureName = textureName.Replace("[ID]", mapData.ID.ToString());
        textureName = textureName.Replace("[X]", x.ToString());
        textureName = textureName.Replace("[Y]", y.ToString());
        var path = $"{mapData.BlockTextureFolder}/{textureName}.png";
        return AssetDatabase.LoadAssetAtPath<Texture>(path);
    }
}
