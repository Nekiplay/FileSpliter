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
            Console.Title = "File Spliter";
            Console.WriteLine("Actions:");
            Console.WriteLine("1 - Split");
            Console.WriteLine("2 - UnSplit");
            Console.Write("Select action: ");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                Console.Write("File path: ");
                string filepath = Console.ReadLine();
                filepath = filepath.Replace("\"", "");
                FileInfo file = new FileInfo(filepath);
                if (file.Exists)
                {
                    Console.Write("Splits (int): ");
                    int splits = 2;
                    int.TryParse(Console.ReadLine(), out splits);
                    FileSpliter.FileSpliter spliter = new FileSpliter.FileSpliter(splits);
                    DirectoryInfo directory = new DirectoryInfo(file.Name);
                    if (directory.Exists)
                    {
                        Directory.Delete(file.Name, true);
                    }
                    Directory.CreateDirectory(file.Name);
                    string hex = spliter.Split(file, directory);
                    Console.WriteLine("Success " + file.Name + " splited");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("File not found");
                }
            }
            else if (answer == "2")
            {
                Console.Write("Directory path: ");
                string directory = Console.ReadLine();
                directory = directory.Replace("\"", "");
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                if (directoryInfo.Exists)
                {
                    FileSpliter.FileSpliter spliter = new FileSpliter.FileSpliter(40);
                    Console.Write("Save path: ");
                    string savePath = Console.ReadLine();
                    savePath = savePath.Replace("\"", "");
                    string hex = spliter.UnSplit(directoryInfo, savePath);
                    Console.WriteLine("Success un splited");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Directory not found");
                }
            }
        }
    }
}
