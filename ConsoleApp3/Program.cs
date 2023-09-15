using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            PostFile();
            Console.ReadKey();
        }

        private static async Task<List<string>> GetFile()
        {
            List<string> allText = new List<string>();
            string[] file_list = Directory.GetFiles(@"C:\\Users\\alexerw\\Desktop\\text\\files", "*.xaml");
            foreach (string path in file_list)
            {
                using (FileStream fstream = File.OpenRead(path))
                {
                    byte[] buffer = new byte[fstream.Length];
                    await fstream.ReadAsync(buffer, 0, buffer.Length);
                    string textFromFile = Encoding.Default.GetString(buffer);
                    textFromFile = textFromFile.Remove(0, 3);
                    allText.Add(Path.GetFileName(path));
                    allText.Add(textFromFile);
                }
            }
            return allText;
        }

        private static async void PostFile()
        {
            string path = @"C:\\Users\\alexerw\\Desktop\text\\allxamlf.txt";
            List<string> allText = await GetFile();
            foreach(string oneText in allText)
            {
                Console.WriteLine(oneText);
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteLineAsync(oneText);
                }
            }
            Console.WriteLine("OK");
        }
    }
}
