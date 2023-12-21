using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameScripts.RunTime.Buff
{
    public class BuffHandler : MonoBehaviour
    {
        public LinkedList<BuffInfo> buffList = new();
        private List<BuffInfo> _deleteBuffList = new List<BuffInfo>();

        public void AddBuff(BuffInfo buffInfo)
        {
            var findBuffInfo = FindBuff(buffInfo.buffData.id);

            if (findBuffInfo is not  null)
            {
                if (findBuffInfo.curStack < buffInfo.buffData.maxStack)
                {
                    findBuffInfo.curStack++;
                    switch (findBuffInfo.buffData.buffUpdateTime)
                    {
                        case BuffTimeUpdateEnum.Add:
                            findBuffInfo.durationTimer += findBuffInfo.buffData.duration;
                            break;
                        case BuffTimeUpdateEnum.Replace:
                            findBuffInfo.durationTimer = findBuffInfo.buffData.duration;
                            break;
                    }
                    findBuffInfo.buffData.OnCreate.Apply(findBuffInfo);
                }
            }
            else
            {
                buffInfo.durationTimer = buffInfo.buffData.duration;
                buffInfo.buffData.OnCreate.Apply(buffInfo);
                buffList.AddLast(buffInfo);

                //排序
                buffList.OrderBy(x => x.buffData.priority);
            }
        }

        public void RemoveBuff(BuffInfo buffInfo)
        {
            switch (buffInfo.buffData.buffRemoveStackUpdateEnum)
            {
                case BuffRemoveStackUpdateEnum.Clear:
                    buffInfo.buffData.OnRemove.Apply(buffInfo);
                    buffList.Remove(buffInfo);
                    break;
                case BuffRemoveStackUpdateEnum.Reduce:

                    buffInfo.curStack--;
                    buffInfo.buffData.OnRemove.Apply(buffInfo);
                    
                    if (buffInfo.curStack == 0)
                    {
                        buffList.Remove(buffInfo);
                    }
                    else
                    {
                        buffInfo.durationTimer = buffInfo.buffData.duration;
                    }
                    break;
            }
        }

        private BuffInfo FindBuff(int buffDataID)
        {
            return buffList.FirstOrDefault(buffInfo => buffInfo.buffData.id == buffDataID);
        }

        private void BuffTickAndRemove()
        {
            _deleteBuffList.Clear();
            foreach (var buffInfo in buffList)
            {
                if (buffInfo.buffData.OnTick is not null)
                {
                    if (buffInfo.tickTimer  < 0)
                    {
                        buffInfo.buffData.OnTick.Apply(buffInfo);
                        buffInfo.tickTimer = buffInfo.buffData.tickTime;
                    }
                    else
                    {
                        buffInfo.tickTimer -= Time.deltaTime;
                    }
                }

                if (buffInfo.durationTimer < 0)
                {
                    _deleteBuffList.Add(buffInfo);
                }
                else
                {
                    buffInfo.durationTimer -= Time.deltaTime;
                }
            }

            foreach (var buffInfo in _deleteBuffList)
            {
                RemoveBuff(buffInfo);
            }
        }
    }
}