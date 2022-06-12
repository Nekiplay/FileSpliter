using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSpliter
{
    public class FileSpliter
    {
        private int splits = 0;
        public FileSpliter(int splits)
        {
            this.splits = splits;
        }

        public string Split(FileInfo fileInfo, DirectoryInfo save)
        {
            int needFiles = splits;
            string hex = HexUtils.ByteArrayToString(File.ReadAllBytes(fileInfo.FullName));
            List<string> hexSplited = Split(hex, hex.Length / needFiles).ToList();
            Console.WriteLine(hexSplited.Count);
            List<string> names = new List<string>();
            for (int i = 0; i < hexSplited.Count; i++)
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name);
                string file = save.FullName + "\\" + filename + "[" + i + "].hex";
                names.Add(file);
            }
            names.Sort();
            int i2 = 0;
            foreach (string name in names)
            {
                File.Create(name).Close();
                File.WriteAllText(name, hexSplited[i2]);
                i2++;
            }
            return hex;
        }

        public string UnSplit(DirectoryInfo splited, string save, bool remove = false)
        {
            string hex = "";
            string[] files = Directory.GetFiles(splited.FullName);
            List<string> s = new List<string>();
            s.AddRange(files);
            s.Sort();
            foreach (string file in s)
            {
                Console.WriteLine(file);
                hex += File.ReadAllText(file);
                if (remove) try { File.Delete(file); } catch { }
            }
            byte[] hexbytes = HexUtils.StringToByteArray(hex);
            if (remove) try { Directory.Delete(splited.FullName, true); } catch { }
            File.Create(save).Close();
            File.WriteAllBytes(save, hexbytes);
            return hex;
        }

        IEnumerable<string> Split(string text, int size)
        {
            for (var i = 0; i < text.Length; i += size)
                yield return text.Substring(i, Math.Min(size, text.Length - i));
        }
    }
}
