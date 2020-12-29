using System;
using System.IO;


namespace DateFile
{
    class Program
    {
        static void Main(string[] args)
        {

            File.AppendAllText("startup.txt", DateTime.Now.ToString("HH:mm:ss "));
            File.AppendAllText("startup.txt", Environment.NewLine);

        }
    }
}
