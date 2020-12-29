using System;
using System.IO;


namespace BinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сколько значений Вы будете вводить ?");
            byte[] array = new byte[nums().Item1];
            int num;
            bool end = true;
            Console.WriteLine("Теперь введите сами значения");
            for (int i = 0; i < array.Length; i++)
            {
                (num, end) = nums();
                if (!end) break;
                array[i] = (byte)num;
            }
            File.WriteAllBytes("bytes.bin", array);
            ReadByte();


        }
        static (int, bool) nums()
        {
            string nums;
            int num;
            Console.WriteLine("Введите значение от 0 до 255 или Q для выхода");
            while (true)
            {
                try
                {
                    nums = Console.ReadLine();
                    if (nums == "q" || nums == "Q") return (0, false);
                    num = int.Parse(nums);
                    if (int.Parse(nums) < 0 || int.Parse(nums) > 255)
                    {
                        Console.WriteLine("Введенно не корректное значение. Нужно от 0 до 255");
                        continue;
                    }
                    else return (num, true);
                }
                catch
                {
                    Console.WriteLine("Введенно не корректное значение. Нужно от 0 до 255");
                }
            }

        }
        static void ReadByte()
        {
            byte[] fromFile = File.ReadAllBytes("bytes.bin");
            foreach (byte i in fromFile)
            {
                if (i == null) break;
                Console.Write(i + " ");
            }
            Console.ReadLine();
        }
    }
}
