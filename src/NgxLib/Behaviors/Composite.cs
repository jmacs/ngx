namespace NgxLib.Behaviors
{
    public abstract class Composite : Behavior
    {
        protected Behavior[] Children { get; set; }

        protected Composite(params Behavior[] children)
        {
            Children = children;
        }
    }
}