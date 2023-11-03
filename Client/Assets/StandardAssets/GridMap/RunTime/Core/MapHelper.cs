namespace GridMap
{
    /// <summary>
    /// 地图助手
    /// </summary>
    internal abstract class MapHelper
    {
        public MapManager Map;


        internal virtual void OnInit()
        {

        }

        /// <summary>
         /// 当设置地图数据
         /// </summary>
         /// <param name="mapData"></param>
         internal virtual void OnSetMapData(MapData mapData)
         {
             
         }
         
         /// <summary>
         /// 当块Block参数发生改变
         /// </summary>
         internal virtual void OnBlockParamsChanged(int width,int height, float size)
         {
             
         }
         
         /// <summary>
         /// 网格图参数发生更改时
         /// </summary>
         /// <param name="width"></param>
         /// <param name="height"></param>
         /// <param name="size"></param>
         internal virtual void OnGridGraphParamsChanged(int width,int height, float size)
         {
             
         }
         
         internal virtual void OnRelease()
         {
             
         }
    }
}
