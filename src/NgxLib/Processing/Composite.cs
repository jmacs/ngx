namespace NgxLib.Processing
{
    public class Composite : Process
    {
        protected Process[] Children { get; set; }

        protected Composite(params Process[] children)
        {
            Children = children;
        }
    }
}