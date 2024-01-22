using System.Collections.Generic;
using DG.Tweening;
using GameScripts.RunTime.War;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 移动处理
    /// </summary>
    public class MoveHandler: CmdHandlerBase<Move>
    {
        private List<Warrior> _warriors = new();

   

        protected override void OnExecute()
        {
            GetExecutors(Data.executor,_warriors);

            var atkObj = Unit.AtkObj;
            var vicObj = Unit.GetVicObjFirst();
            
            foreach (var warrior in _warriors)
            {
                if (Data.move_type == MoveType.Circle)
                {
                    MoveCircle(atkObj, vicObj, warrior,Data.circle,Data.move_time);
                }
                else if (Data.move_type == MoveType.Jump)
                {
                    MoveJump(atkObj, vicObj, warrior,Data.jump,Data.move_time);
                }
                else
                {
                    MoveLine(atkObj, vicObj, warrior, Data.line,Data.move_time);
                }
           
            }

            Main.m_Main.DelayExecute(SetCompleted, Data.move_time);
        }

        private void MoveCircle(Warrior atkObj, Warrior vicObj, Warrior executor,ComplexMovementCircle complexMovementCircle, float commandDataMoveTime)
        {

            var max = Mathf.Clamp(complexMovementCircle.lerp_cnt,1, 5);
           var path = new List<Vector3>();

           var beginRelative = complexMovementCircle.begin_relative;
           var endRelative = complexMovementCircle.end_relative;
           
            for (var i = 0; i < max; i++)
            {
                var iLerp = i / (max - 1);
                var depth = Mathf.Lerp( beginRelative.Depth, endRelative.Depth, iLerp);
                var dis = Mathf.Lerp( beginRelative.RelativeDistance, endRelative.RelativeDistance, iLerp);
                var angle = Mathf.Lerp( beginRelative.RelativeAngle, endRelative.RelativeAngle, iLerp);

                var complexPosition = new ComplexPosition
                {
                    BasePosition = beginRelative.BasePosition,
                    Depth = depth,
                    RelativeAngle = angle,
                    RelativeDistance = dis
                };
                var pathNode = MagicTools.CalcPos(complexPosition, atkObj, vicObj,false);
                path.Add(pathNode);
            }

            executor.Entity.transform.DOPath(path.ToArray(), commandDataMoveTime, PathType.CatmullRom, PathMode.Ignore,5, Color.green);
        }

        private void MoveJump(Warrior atkObj, Warrior vicObj, Warrior executor, ComplexMovementJump complexMovementJump,float commandDataMoveTime)
        {
            var beginPos = Vector3.zero;
            if (complexMovementJump.begin_type == LocationType.Current)
            {
                beginPos = executor.Entity.transform.localPosition;
            }
            else if (complexMovementJump.begin_type == LocationType.Prepare)
            {
                
            }
            else
            {
                beginPos = MagicTools.CalcPos(complexMovementJump.begin_relative, atkObj, vicObj,complexMovementJump.calc_face);
            }
            
            executor.Entity.transform.localPosition = beginPos;
            if (commandDataMoveTime <= 0) return;
            
            var endPos = beginPos;
            if (complexMovementJump.end_type == LocationType.Prepare)
            {
                
            }
            else if (complexMovementJump.end_type == LocationType.Relative)
            {
                endPos = MagicTools.CalcPos(complexMovementJump.end_relative, atkObj, vicObj,complexMovementJump.calc_face);
            }

            //如果面向终点
            if (complexMovementJump.look_at_pos)
            {
                executor.LookAtPos(endPos,commandDataMoveTime);
            }

            // if (executor.SetRotateNode)
            // {
            //     //旋转 暂时不处理
            // }

            executor.Entity.transform.DOLocalJump(endPos, complexMovementJump.jump_power, complexMovementJump.jump_num,
                commandDataMoveTime).SetEase(complexMovementJump.ease_type);
        }
        
        private void MoveLine(Warrior atkObj, Warrior vicObj, Warrior executor, ComplexMovementLine complexMovementLine,float commandDataMoveTime)
        {
          
            var beginPos = Vector3.zero;
            if (complexMovementLine.begin_type == LocationType.Current)
            {
                beginPos = executor.Entity.transform.localPosition;
            }
            else if (complexMovementLine.begin_type == LocationType.Prepare)
            {
                // complexMovementLine.begin_prepare
                // beginPos = 
            }
            else
            {
                beginPos = MagicTools.CalcPos(complexMovementLine.begin_relative, atkObj, vicObj,complexMovementLine.calc_face);
            }

            executor.Entity.transform.localPosition = beginPos;
            if (commandDataMoveTime <= 0) return;

            var endPos = beginPos;
            if (complexMovementLine.end_type == LocationType.Prepare)
            {
                
            }
            else if (complexMovementLine.end_type == LocationType.Relative)
            {
                endPos = MagicTools.CalcPos(complexMovementLine.end_relative, atkObj, vicObj,complexMovementLine.calc_face);
            }

            //如果面向终点
            if (complexMovementLine.look_at_pos)
            {
                executor.LookAtPos(endPos,commandDataMoveTime);
            }

            executor.Entity.transform.DOLocalMove(endPos, commandDataMoveTime).SetEase(complexMovementLine.ease_type);
        }
    }
}