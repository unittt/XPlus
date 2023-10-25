using GridMap;
using UnityEditor;
using UnityEngine;

public class GridDemo : MonoBehaviour
{

    public GridMapManager GridMapManager;
    public TextAsset DataAsset;
    
    void Start()
    {
        var mapData = MapData.Deserialize(DataAsset.bytes);
        GridMapManager.GridTextFunc = GetGridTexture;
        GridMapManager.SetGridMapData(mapData);
    }
    
    private Texture GetGridTexture(MapData mapData, int x, int y)
    {
        var textureName = GridMapConfig.Instance.TextureNameRule;
        textureName = textureName.Replace("[ID]", mapData.ID.ToString());
        textureName = textureName.Replace("[X]", x.ToString());
        textureName = textureName.Replace("[Y]", y.ToString());
        var path = $"{mapData.TextureFolder}/{textureName}.png";
        return AssetDatabase.LoadAssetAtPath<Texture>(path);
    }
}
