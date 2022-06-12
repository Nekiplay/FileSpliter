using FileSpliter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Test2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Load();
            Console.ReadLine();
        }
        static void Load()
        {
            ChunkManager chunkManager = new ChunkManager();
            List<Chunk> chunks = new List<Chunk>();
            string[] chunksNames = Directory.GetFiles(Environment.CurrentDirectory, "*.chunk");
            foreach (string chunkName in chunksNames)
            {
                Chunk chunk = Chunk.FromJson(File.ReadAllText(chunkName));
                chunks.Add(chunk);
            }
            chunks = chunkManager.Sort(chunks);
            foreach (Chunk chunk in chunks)
            {
                Console.WriteLine(chunk.index);
            }
            Console.WriteLine("Getting hex...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            byte[] fileBytes = chunkManager.GetHex(chunks);
            File.Create("original.jar").Close();
            File.WriteAllBytes("original.jar", fileBytes);
            stopwatch.Stop();
            Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
        }
        
        static void Save()
        {
            ChunkCreator chunkCreator = new ChunkCreator();
            FileInfo info = new FileInfo(@"E:\Minecraft\AC Bypass [1.17.1]\paper-1.12.2-1620.jar");
            string name = Path.GetFileNameWithoutExtension(info.Name);
            Chunk[] chunks = chunkCreator.GetFileChunks(info, 20);
            foreach (Chunk chunk in chunks)
            {
                string chunkFileName = name + " [" + chunk.index + "]" + ".chunk";
                File.Create(chunkFileName).Close();
                File.WriteAllText(chunkFileName, chunk.ToJson());
            }
        }
        
    }
}
