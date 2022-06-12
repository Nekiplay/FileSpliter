using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileSpliter.FileSpliter spliter = new FileSpliter.FileSpliter(splits: 8);
            FileInfo file = new FileInfo(@"E:\Minecraft\AC Bypass [1.17.1]\plugins\Vulcan 2.6.5.jar");
            DirectoryInfo directory = new DirectoryInfo(file.Name + "1");
            if (!directory.Exists)
            {
                Directory.CreateDirectory(file.Name + "1");
            }
            string hex_split = spliter.Split(file, directory);


            string hex_unsplit = spliter.UnSplit(directory, "Vulcan 2.6.5.jar", true);
            if (hex_split == hex_unsplit)
            {
                Console.WriteLine("Мы равны");
            }
            else
            {
                Console.WriteLine("Мы не равны");
            }
            Console.ReadKey();
        }
    }
}
