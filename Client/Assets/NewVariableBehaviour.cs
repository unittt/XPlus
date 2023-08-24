using HT.Framework;
using UnityEngine;

[DisallowMultipleComponent]
public partial class NewVariableBehaviour :  VariableBehaviour
{
    #region parameter
 public System.Boolean dsads { get; private set; }
#endregion
    
    protected override void Awake()
    {
        base.Awake();
        #region Awake
            dsads = Container.Get<System.Boolean>("dsads");
#endregion
		
		OnAwake();
    }
	
	 partial void OnAwake();
}
