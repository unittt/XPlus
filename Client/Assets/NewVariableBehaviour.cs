using HT.Framework;
using UnityEngine;

[DisallowMultipleComponent]
public partial class NewVariableBehaviour :  VariableBehaviour
{
    #region parameter
 public UnityEngine.Transform asdas;
 public System.String Ab;
#endregion
    
    protected override void Awake()
    {
        base.Awake();
        #region Awake
            asdas = Container.Get<UnityEngine.Transform>("asdas");
            Ab = Container.Get<System.String>("Ab");
#endregion
		
		OnAwake();
    }
	
	 partial void OnAwake();
}
