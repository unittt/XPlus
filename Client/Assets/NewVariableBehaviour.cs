using HT.Framework;
using UnityEngine;

[DisallowMultipleComponent]
public partial class NewVariableBehaviour :  VariableBehaviour
{
    #region parameter
 public UnityEngine.Vector2 xzcxz { get; private set; }
#endregion
    
    protected override void Awake()
    {
        base.Awake();
        #region Awake
            xzcxz = Container.Get<UnityEngine.Vector2>("xzcxz");
#endregion
		
		OnAwake();
    }
	
	 partial void OnAwake();
}
