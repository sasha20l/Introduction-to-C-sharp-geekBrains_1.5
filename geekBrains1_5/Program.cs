using System;
using System.IO;


namespace geekBrains1_5
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText("text.txt", Console.ReadLine());
        }
    }
}
