using GameScripts.RunTime.Audio;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 播放音效
    /// </summary>
    public class PlaySoundHandler : CmdHandlerBase<PlaySound>
    {
        protected override void OnFill(PlaySound commandData)
        {
            AudioManager.Current.PlayEffect(commandData.sound.SoundPath);
            SetCompleted();
        }
    }
}