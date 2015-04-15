namespace NgxLib
{
    public interface INgxCommand
    {
        long GetMessageKey();
        void Execute(NgxContext context, NgxMessage args);
    }
}
