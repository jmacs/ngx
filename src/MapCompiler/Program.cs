using System;

namespace MapCompiler
{
    class Program
    {
        // this program compiles Tiled maps into a custom format

        static void Main(string[] args)
        {
            try
            {
                var compiler = new CompilerService();
                compiler.Compile(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> {0}: {1}", ex.GetType(), ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("done");
            Console.Read();
        }
    }
}
