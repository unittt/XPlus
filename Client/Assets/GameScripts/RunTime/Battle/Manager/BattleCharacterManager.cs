// using System.Collections.Generic;
// using GameScripts.RunTime.Battle.Avatar;
// using GameScripts.RunTime.Battle.Character;
// using GameScripts.RunTime.Battle.Model;
// using GameScripts.RunTime.Battle.Report.Data;
// using HT.Framework;
// using UnityEngine;
//
// namespace GameScripts.RunTime.Battle.Manager
// {
//     public class BattleCharacterManager : SingletonBase<BattleCharacterManager>
//     {
//         public List<BatCharacter> attackers { get; private set; }
//         public int attackersCount { get; private set; }
//         public List<BatCharacter> defenders { get; private set; }
//         public int defendersCount { get; private set; }
//         
//         public BatCharacter mainRole { get; private set; }
//         public BatCharacter mainPet { get; private set; }
//         
//         private List<BatCharacter> mCharacters = new List<BatCharacter>();
//         private int mCharactersCount = 0;
//         
//         private BatRoundStageData mCurRoundBehavData = null;
//         private int mCurRoundBehavDataIdx = -1;
//
//         private BatRoundBehaveData mCurRoundStartBehavItemData = null;
//         private int mCurRoundStartBehavItemDataIdx = -1;
//
//         private BatRoundBehaveData mCurRoundExeBehavItemData = null;
//         private int mCurRoundExeBehavItemDataIdx = -1;
//
//         private BatRoundBehaveData mCurRoundDefenceBehavItemData = null;
//         private int mCurRoundDefenceBehavItemDataIdx = -1;
//
//         private BatRoundBehaveData mCurRoundAdjustBehavItemData = null;
//         private int mCurRoundAdjustBehavItemDataIdx = -1;
//
//         private BatRoundBehaveData mCurRoundEndBehavItemData = null;
//         private int mCurRoundEndBehavItemDataIdx = -1;
//
//         private bool mIsInited = false;
//         private bool mIsReadyToFight = false;
//
//         public BattleCharacterManager()
//         {
//             attackers = new List<BatCharacter>();
//             attackersCount = 0;
//             defenders = new List<BatCharacter>();
//             defendersCount = 0;
//             mIsReadyToFight = false;
//             mIsInited = false;
//         }
//
//         public void StartRound()
//         {
//             mCurRoundBehavData = null;
//             mCurRoundBehavDataIdx = -1;
//             mCurRoundStartBehavItemData = null;
//             mCurRoundStartBehavItemDataIdx = -1;
//             mCurRoundExeBehavItemData = null;
//             mCurRoundExeBehavItemDataIdx = -1;
//             mCurRoundDefenceBehavItemData = null;
//             mCurRoundDefenceBehavItemDataIdx = -1;
//             mCurRoundAdjustBehavItemData = null;
//             mCurRoundAdjustBehavItemDataIdx = -1;
//             mCurRoundEndBehavItemData = null;
//             mCurRoundEndBehavItemDataIdx = -1;
//             
//             BattleModel.Current.curRoundStatus = BattleRoundStatusType.ROUND_INIT_START;
//             
//             if (mIsInited)
//             {
//                 UpdateCharactersData();
//                 FadeInAllCharacters();
//             }
//             else
//             {
//                 Init();
//             }
//             
//             mainRole = GetMainRole();
//             mainPet = GetMainPet();
//
//             BattleModel.Current.curRoundStatus = BattleRoundStatusType.ROUND_INIT_PROGRESS;
//         }
//         
//         
//           public void Update()
//         {
//             UpdateCharacters(attackers, attackersCount);
//             UpdateCharacters(defenders, defendersCount);
//
//             if (!isReadyToFight)
//             {
//                 return;
//             }
//
//             if (BattleModel.Current.maxModelHeight == 0)
//             {
//                 for (int i = 0; i < attackersCount; i++)
//                 {
//                     BattleModel.Current.maxModelHeight = Mathf.Max(BattleModel.ins.maxModelHeight, attackers[i].displayModel.totalHeight);
//                 }
//
//                 for (int i = 0; i < defendersCount; i++)
//                 {
//                     BattleModel.Current.maxModelHeight = Mathf.Max(BattleModel.ins.maxModelHeight, defenders[i].displayModel.totalHeight);
//                 }
//
//                 for (int i = 0; i < attackersCount; i++)
//                 {
//                     if (!attackers[i].isDestroied)
//                     {
//                         attackers[i].CreateAvatarName();
//                         attackers[i].CreateBloodBar();
//                     }
//                 }
//
//                 for (int i = 0; i < defendersCount; i++)
//                 {
//                     if (!defenders[i].isDestroied)
//                     {
//                         defenders[i].CreateAvatarName();
//                         defenders[i].CreateBloodBar();
//                     }
//                 }
//             }
//
//             if (BattleModel.Current.curRoundStatus == BattleRoundStatusType.ROUND_INIT_PROGRESS)
//             {
//                 if (isReadyToFight)
//                 {
//                     BattleModel.ins.curRoundStatus = BattleRoundStatusType.ROUND_INIT_FINISH;
//                 }
//             }
//
//             if (BattleModel.Current.curRoundStatus == BattleRoundStatusType.ROUND_INIT_FINISH)
//             {
//                 //ClientLog.Log("StartRoundStart");
//                 StartRoundStart();
//             }
//
//             if (BattleModel.Current.curRoundStatus == BattleRoundStatusType.ROUND_START_PROGRESS)
//             {
//                 if (isRoundStartFinish)
//                 {
//                     //ClientLog.Log("FinishRoundStart");
//                     BattleModel.Current.curRoundStatus = BattleRoundStatusType.ROUND_START_FINISH;
//                 }
//                 else
//                 {
//                     //ClientLog.Log("ProcessRoundStart");
//                     ProcessRoundStart();
//                 }
//             }
//
//             if (BattleModel.Current.curRoundStatus == BattleRoundStatusType.ROUND_START_FINISH)
//             {
//                 //ClientLog.Log("StartRoundProgress");
//                 StartRoundProgress();
//             }
//
//             if (BattleModel.Current.curRoundStatus == BattleRoundStatusType.ROUND_PROGRESS_PROGRESS)
//             {
//                 if (isRoundProgressFinish)
//                 {
//                     //ClientLog.Log("FinishRoundProgress");
//                     BattleModel.ins.curRoundStatus = BattleRoundStatusType.ROUND_PROGRESS_FINISH;
//                 }
//                 else
//                 {
//                     //ClientLog.Log("ProcessRoundProgress");
//                     ProcessRoundProgress();
//                 }
//             }
//
//             if (BattleModel.Current.curRoundStatus == BattleRoundStatusType.ROUND_PROGRESS_FINISH)
//             {
//                 StartRoundEnd();
//             }
//
//             if (BattleModel.Current.curRoundStatus == BattleRoundStatusType.ROUND_END_PROGRESS)
//             {
//                 if (isRoundEndFinish)
//                 {
//                     BattleModel.Current.curRoundStatus = BattleRoundStatusType.ROUND_END_FINISH;
//                     if (!BattleModel.Current.curRoundData.isFinalRound)
//                     {
//                         BattleModel.Current.curRoundWaitTimeLeft = BattleDef.MANUAL_ROUND_CD_SECONDS;
//                     }
//
//                     for (int i = 0; i < attackersCount; i++)
//                     {
//                         if (attackers[i].curAnimName == AvatarBase.ANIM_NAME_DEFENSE)
//                         {
//                             attackers[i].PlayAnimation(AvatarBase.ANIM_NAME_IDLE);
//                         }
//                     }
//
//                     for (int i = 0; i < defendersCount; i++)
//                     {
//                         if (defenders[i].curAnimName == AvatarBase.ANIM_NAME_DEFENSE)
//                         {
//                             defenders[i].PlayAnimation(AvatarBase.ANIM_NAME_IDLE);
//                         }
//                     }
//                     
//                 }
//                 else
//                 {
//                     //ClientLog.Log("ProcessRoundEnd");
//                     ProcessRoundEnd();
//                 }
//             }
//         }
//           
//           
//           private void StartRoundStart()
//           {
//               // ClientLog.Log("--------------------DO ROUND_START--------------------");
//               BattleModel.Current.curRoundStatus = BattleRoundStatusType.ROUND_START_START;
//               BattleModel.Current.curRoundStatus = BattleRoundStatusType.ROUND_START_PROGRESS;
//               mCurRoundBehavDataIdx = -1;
//           }
//           
//           private void ProcessRoundStart()
//           {
//               ProcessRoundBehav(BattleModel.Current.curRoundData.startDatas);
//           }
//         
//     }
// }