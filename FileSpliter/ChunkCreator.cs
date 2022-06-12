using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSpliter
{
    public class ChunkCreator
    {
        public Chunk[] GetFileChunks(FileInfo fileinfo, int maxChunks = 2)
        {
            List<Chunk> chunks = new List<Chunk>();
            string hex = HexUtils.ByteArrayToString(File.ReadAllBytes(fileinfo.FullName));
            List<string> hexSplited = GetSplit(hex, maxChunks);
            for (int i = 0; i < hexSplited.Count; i++)
            {
                Chunk chunk = new Chunk(hexSplited[i], i);
                chunks.Add(chunk);
            }
            return chunks.ToArray();
        }

        public Chunk[] GetStringChunks(string str, int maxChunks = 2)
        {
            List<Chunk> chunks = new List<Chunk>();
            List<string> hexSplited = GetSplit(str, maxChunks);
            for (int i = 0; i < hexSplited.Count; i++)
            {
                Chunk chunk = new Chunk(hexSplited[i], i);
                chunks.Add(chunk);
            }
            return chunks.ToArray();
        }

        private List<string> GetSplit(string str, int maxChunks = 2) {
            int needFiles = maxChunks;
            if (str.Length < maxChunks)
            {
                needFiles = str.Length;
            }
            List<string> hexSplited = HexUtils.Split(str, str.Length / needFiles).ToList();
            //if (hexSplited.Count > needFiles)
            //{
            //    needFiles = needFiles - (hexSplited.Count - needFiles);
            //    hexSplited = HexUtils.Split(str, str.Length / needFiles).ToList();
            //}
            return hexSplited;
        }
    }
}
