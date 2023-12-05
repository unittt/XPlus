using UnityEngine;

namespace GameScripts.RunTime.Battle.Avatar
{
    public class AvatarBase
    {
        
        
        public string curAnimName { get; private set; }
        public string displayModelId { get; private set; }
        public AvatarBase()
        {
            // isEnableWing = true;
            // isEnableWeapon = true;
            // isActive = true;
            // isDestroied = false;
        }
        
        public void Init(string displayModelId, bool showShadow = true, bool particlesWritable = true)
        {
            // mInitNeedSetPPR = false;
            // mParticlesWritable = particlesWritable;
            // InitAndLoadDisplayModel(displayModelId, showShadow);
        }
        
        public void Init(string displayModelId, Vector3 pos, Vector3 rot, Transform parent, bool showShadow = true, bool particlesWritable = true)
        {
            // mInitNeedSetPPR = true;
            // mInitPos = pos;
            // mInitRot = rot;
            // mInitParent = parent;
            // mParticlesWritable = particlesWritable;
            // //mInitLayer = layer;
            // InitAndLoadDisplayModel(displayModelId, showShadow);
        }
    }
}