namespace NgxLib.Behaviors
{
    public abstract class Decorator : Behavior
    {
        protected Behavior Child { get; set; }

        protected Decorator(Behavior child)
        {
            Child = child;
        }
    }
}
