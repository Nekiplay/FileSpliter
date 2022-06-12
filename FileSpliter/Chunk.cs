using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSpliter
{
    public class Chunk
    {
        public string data;
        public int index;

        public Chunk(string data, int index)
        {
            this.data = data;
            this.index = index;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Chunk FromJson(string json)
        {
            Chunk chunk = JsonConvert.DeserializeObject<Chunk>(json);
            return chunk;
        }
    }
}
