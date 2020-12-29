using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace ToDoList
{
    class Program
    {
        public static int maxSerialNumber = 0;
        static void Main(string[] args)
        {
            int theChange;
            string title;
            ToDo[] toDo = new ToDo[0];
            try
            {
                toDo = Deserialization();
            }
            catch
            {
                Serialization(toDo);
            }
            ViewToDo(toDo);
            while (true)
            {
                Console.WriteLine("1 - Создать задачу 2 - Изменить задачу 3 - Очистить весь список задач 4 - Выход");
                theChange = Choice(1, 3, "Вы ввели не корректное значение (нужно 1-3)");

                switch (theChange)
                {
                    case 1:
                        Console.WriteLine($"Введите описание задачи № {maxSerialNumber + 1}");
                        title = Console.ReadLine();
                        toDo = CreateToDo(toDo, title, false);
                        ViewToDo(toDo);
                        Serialization(toDo);
                        break;
                    case 2:
                        if (toDo.Length == 0)
                        {
                            Console.WriteLine($"Нет задач для изменения");
                            continue;
                        }
                        EditToDo(toDo);
                        Serialization(toDo);
                        break;
                    case 3:
                        toDo = new ToDo[0];
                        maxSerialNumber = 0;
                        Serialization(toDo);
                        Console.WriteLine("Список задач очищен");
                        break;
                    case 4:
                        Serialization(toDo);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
        }

        static ToDo[] ToArray(List<ToDo> toDo)
        {
            //return (ToDo[])toDo.ToArray();
            int nums = 0;
            ToDo[] array = new ToDo[toDo.Count];
            foreach(ToDo todo in toDo)
            {
                array[nums] = todo;
                nums++;

            }
            return array;
        }

        static List<ToDo> ToList(ToDo[] toDo)
        {
            List<ToDo> listToDo = new List<ToDo>();
            foreach (ToDo todo in toDo)
            {
                listToDo.Add(todo);

            }
            return listToDo;
           
        }

        static ToDo[] Deserialization()
        {
            string json = File.ReadAllText("ToDo.json");
            return JsonSerializer.Deserialize<ToDo[]>(json);
        }

        static void Serialization(ToDo[] toDo)
        {
            string json = JsonSerializer.Serialize(toDo);
            File.WriteAllText("ToDo.json", json);
        }

        static void ViewToDo(ToDo[] toDo)
        {
            string active;
            foreach(ToDo todo in toDo)
            {
                active = todo.IsDone ? "[X]" : "   ";
                Console.WriteLine($"{active}  {todo.SerialNumber}. {todo.Title}");
            }
        }

        static ToDo[] CreateToDo(ToDo[] toDo, string title, bool IsDone)
        {
            List<ToDo> todo = ToList(toDo);
            maxSerialNumber++;
            todo.Add(new ToDo() { Title = title, IsDone = false, SerialNumber = maxSerialNumber});
            return toDo = ToArray(todo);
        }

        static void EditToDo(ToDo[] toDo)
        {
            int aTask;
            int theChange;
            int index;
            string text;

            ViewToDo(toDo);
            Console.WriteLine($"Выберите номер задачи 1 - {maxSerialNumber}");
            aTask = Choice(1, maxSerialNumber, "Такой задачи тут нету, введите корректное значение");
            Console.WriteLine("1 - Изменить описание задачи 2 - Отметить что задача выполнена 3 - Отметить что задача требует выполнение ");
            theChange = Choice(1, 3, "введите корректное значение (1-3)");
            index = BruteForce(toDo, aTask);

            switch (theChange)
            {
                case 1:
                    Console.WriteLine($"Введите описание выбранной задачи");
                    toDo[index].Title = Console.ReadLine();
                    break;
                case 2:
                    toDo[index].IsDone = true;
                    break;
                case 3:
                    toDo[index].IsDone = false;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            ViewToDo(toDo);
        }

        

        static int BruteForce(ToDo[] Todo, int nums)
        {
            for (int i = 0; i < Todo.Length; i++)
            {
                if (Todo[i].SerialNumber == nums) return i;

            }
            return -1;
        }

        static int Choice(int minChoice, int maxChoice, string text)
        {
            int nums;

            while (true)
            {
                try
                {
                    nums = int.Parse(Console.ReadLine());
                } catch
                {
                    Console.WriteLine("Введите одно числовое значение!");
                    continue;
                }

                if (nums > (minChoice-1) ||  nums < (maxChoice+1)) break;
                else Console.WriteLine($"{text}");
            }
            return nums;
        }
    }
        

    class ToDo
    {
        public string Title;
        public bool IsDone;
        public int SerialNumber;

        public ToDo()
        {
            Title = "1";
            IsDone = false;
            SerialNumber = 1;

        }

    }
}
