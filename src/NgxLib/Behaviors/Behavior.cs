namespace NgxLib.Behaviors
{
    public abstract class Behavior
    {
        public virtual BahviorStatus Update()
        {
            return BahviorStatus.Invalid;
        }
    }
}