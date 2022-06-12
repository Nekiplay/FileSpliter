using FileSpliter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Console.WriteLine("1 - Create chunks");
            Console.WriteLine("2 - Create file from chunks");
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
                    Console.Write("Max chunks (int): ");
                    int splits = 2;
                    int.TryParse(Console.ReadLine(), out splits);
                    ChunkCreator chunkCreator = new ChunkCreator();
                    DirectoryInfo directory = new DirectoryInfo(file.Name);
                    if (directory.Exists)
                    {
                        Directory.Delete(file.Name, true);
                    }
                    Directory.CreateDirectory(file.Name);
                    Chunk[] chunks = chunkCreator.GetFileChunks(file, splits);
                    string name = Path.GetFileNameWithoutExtension(file.Name);
                    foreach (Chunk chunk in chunks)
                    {
                        string chunkFileName = file.Name + "//" + name + " [" + chunk.index + "]" + ".chunk";
                        File.Create(chunkFileName).Close();
                        File.WriteAllText(chunkFileName, chunk.ToJson());
                    }
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
                    Console.Write("Save path: ");
                    string savePath = Console.ReadLine();
                    savePath = savePath.Replace("\"", "");
                    ChunkManager chunkManager = new ChunkManager();
                    List<Chunk> chunks = new List<Chunk>();
                    string[] chunksNames = Directory.GetFiles(directoryInfo.FullName, "*.chunk");
                    foreach (string chunkName in chunksNames)
                    {
                        Chunk chunk = Chunk.FromJson(File.ReadAllText(chunkName));
                        chunks.Add(chunk);
                    }
                    chunks = chunkManager.Sort(chunks);
                    Console.WriteLine("Getting hex...");
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    byte[] fileBytes = chunkManager.GetHex(chunks);
                    File.Create(savePath).Close();
                    File.WriteAllBytes(savePath, fileBytes);
                    stopwatch.Stop();
                    Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
                }
                else
                {
                    Console.WriteLine("Directory not found");
                }
            }
            Console.ReadLine();
        }
    }
}
