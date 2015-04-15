using System;
using System.IO;
using System.Xml;

namespace MapCompiler
{
    public class CompilerService
    {
        public void Compile(string path)
        {
            Console.WriteLine("Opening directory {0}", path);

            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles("*.tmx", SearchOption.AllDirectories);

            Console.WriteLine("{0} tmx files found", files.Length);

            var processor = new TiledProcessor();

            foreach (var file in files)
            {               
                var map = processor.Process(file.FullName);
                var filepath = string.Format("{0}/{1}.map", file.Directory.FullName, map.MID);

                using (var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                {
                    map.Serialize(stream);    
                }
            }
        }

        
        
    }
}
