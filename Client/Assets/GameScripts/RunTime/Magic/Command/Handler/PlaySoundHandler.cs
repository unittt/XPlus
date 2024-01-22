using GameScripts.RunTime.Audio;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 播放音效
    /// </summary>
    public class PlaySoundHandler : CmdHandlerBase<PlaySound>
    {
        
        protected override void OnExecute()
        {
            AudioManager.Current.PlayEffect(Data.sound.SoundPath);
            SetCompleted();
        }
    }
}