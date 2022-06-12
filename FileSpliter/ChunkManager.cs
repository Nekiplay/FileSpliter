using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSpliter
{
    public class ChunkManager
    {
        public Chunk[] Sort(Chunk[] chunks)
        {
            return chunks.OrderBy(c => c.index).ToArray();
        }

        public List<Chunk> Sort(List<Chunk> chunks)
        {
            return chunks.OrderBy(c => c.index).ToList();
        }
        public byte[] GetHex(List<Chunk> chunks)
        {
            string hex = "";
            foreach (Chunk chunk in chunks)
            {
                hex += chunk.data;
            }
            return HexUtils.ParseHex(hex);
        }
        
        public byte[] GetHex(Chunk[] chunks)
        {
            return GetHex(chunks.ToList());
        }        
        public string GetString(List<Chunk> chunks)
        {
            string hex = "";
            foreach (Chunk chunk in chunks)
                hex += chunk.data;
            return hex;
        }
        public string GetString(Chunk[] chunks)
        {
            return GetString(chunks.ToList());
        }
    }
}
