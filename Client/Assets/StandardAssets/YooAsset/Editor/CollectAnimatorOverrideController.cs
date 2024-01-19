using System.IO;
using YooAsset.Editor;

public class CollectAnimatorOverrideController : IFilterRule
{
    public bool IsCollectAsset(FilterRuleData data)
    {
        return Path.GetExtension(data.AssetPath) == ".overrideController";
    }
}
