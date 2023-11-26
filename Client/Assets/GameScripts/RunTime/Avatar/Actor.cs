namespace GameScripts.RunTime.Avatar
{
    public abstract class Actor
    {
        //主模型 武器 坐骑

        
        // function CActor.Play(self, sState, startNormalized, endNormalized, func)
        // self:ResetState()
        // sState = self:GetFinalState(sState)
        // self.m_CurState = sState
        //     self.m_ActID = self.m_ActID + 1
        // self:AllModelAnim(CModelBase.Play, sState, startNormalized)
        //     if endNormalized then
        // local fixedTime = ModelTools.NormalizedToFixed(self.m_Shape, self:GetAnimatorIdx(),sState, endNormalized-startNormalized)
        // self:FixedEvent(sState, fixedTime, func)
        // end
        //     end



        private int _actID;
        
        public void Play(string sState,float startNormalized,float endNormalized)
        {
            ResetState();
            sState = GetFinalState(sState);

            var m_CurState = sState;
            _actID += 1;
            
            // self:AllModelAnim(CModelBase.Play, sState, startNormalized)
        }


        private void ResetState()
        {
            
        }


        private string  GetFinalState(string sState)
        {
            if (sState == "dance")
            {
                //隐藏武器
            }
            else
            {
                //显示武器
            }
            
            return sState;
        }
    }
}