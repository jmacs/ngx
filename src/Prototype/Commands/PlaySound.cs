using NgxLib;

namespace Prototype.Commands
{
    public class PlaySound : INgxCommand
    {
        public long GetMessageKey()
        {
            return Msg.Play_Sound;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            context.SoundBank.Play(args.Sound);
        }
    }
}
