using NgxLib;

namespace Prototype.Commands
{
    public class DisableSound : INgxCommand
    {
        public long GetMessageKey() 
        {
            return Msg.Disable_Sound;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            context.SoundBank.Enabled = !context.SoundBank.Enabled;
        }
    }
}
