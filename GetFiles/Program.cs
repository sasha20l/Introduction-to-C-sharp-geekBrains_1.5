using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            GetFile(@"C:\Program Files (x86)\Bethesda Softworks");
        }
        static void GetFile(string file)
        {
            if (file != null)
            {
                try
                {
                    string[] arrayFiles = Directory.GetFileSystemEntries(file, "*", SearchOption.AllDirectories);

                    foreach (string files in arrayFiles)
                    {
                        File.AppendAllText(@"C:\Users\Александр\source\repos\geekBrains1_5\GetFiles\bin\Debug\GetFiles.txt", files);
                        File.AppendAllText(@"C:\Users\Александр\source\repos\geekBrains1_5\GetFiles\bin\Debug\GetFiles.txt", Environment.NewLine);
                        if (!files.Contains("."))
                        {
                            GetFile(files);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"Ошибка при чтении файла - {file}");
                    

                }
            }
        }
    }
}
