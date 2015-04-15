namespace NgxLib.Processing
{
    public abstract class Decorator : Process
    {
        protected Process Child { get; set; }

        protected Decorator(Process child)
        {
            Child = child;
        }
    }
}
