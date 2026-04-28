using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace lab7
{
    public static class FileTasks
    {
        private static Random rnd = new Random();

        private static void FillFileOneNumberPerLine(string filePath, int count)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                    writer.WriteLine(rnd.Next(-100, 101));
            }
        }

        private static void FillFileMultipleNumbersPerLine(string filePath, int linesCount)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < linesCount; i++)
                {
                    int numbersInLine = rnd.Next(3, 7);
                    for (int j = 0; j < numbersInLine; j++)
                        writer.Write(rnd.Next(-50, 51) + " ");
                    writer.WriteLine();
                }
            }
        }

        private static void FillTextFileTask3(string filePath)
        {
            Console.WriteLine("Введите текст построчно (пустая строка — конец ввода):\n");

            List<string> lines = new List<string>();
            int lineNumber = 1;

            string firstLine = InputValidator.ReadNonEmptyLine($"Строка {lineNumber}: ");
            lines.Add(firstLine);
            lineNumber++;

            while (true)
            {
                Console.Write($"Строка {lineNumber}: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) break;

                lines.Add(input);
                lineNumber++;
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines.Count; i++)
                    writer.WriteLine(lines[i]);
            }

            Console.WriteLine($"\nСохранено {lines.Count} строк в '{filePath}'.\n");
        }

        private static void FillBinaryFile(string filePath, int count)
        {
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(filePath)))
            {
                for (int i = 0; i < count; i++)
                    writer.Write(rnd.Next(1, 100));
            }
        }

        private static void FillToysFile(string filePath)
        {
            List<Toy> toys = new List<Toy>();
            string[] names = { "мяч", "конструктор", "кукла", "машинка", "пазл", "робот", "мишка", "юла" };

            for (int i = 0; i < 6; i++)
            {
                Toy toy = new Toy();
                toy.Name = names[rnd.Next(names.Length)];
                toy.Price = rnd.Next(100, 1000);
                toy.MinAge = rnd.Next(0, 8);
                toy.MaxAge = toy.MinAge + rnd.Next(1, 8);
                toys.Add(toy);
            }

            XmlSerializer xml = new XmlSerializer(typeof(List<Toy>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                xml.Serialize(fs, toys);
            }
        }

        public static void SolveTask1()
        {
            Console.Clear();
            Console.WriteLine("Задание 1. Сумма max и min\n");

            string filePath = "task1.txt";
            FillFileOneNumberPerLine(filePath, 12);

            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine($"Файл '{filePath}' заполнен случайными числами ({lines.Length} шт.):");

            int min = int.MaxValue;
            int max = int.MinValue;

            for (int i = 0; i < lines.Length; i++)
            {
                int num = int.Parse(lines[i].Trim());
                Console.Write(num + " ");
                if (num < min) min = num;
                if (num > max) max = num;
            }

            Console.WriteLine($"\n\nМинимум: {min}");
            Console.WriteLine($"Максимум: {max}");
            Console.WriteLine($"Сумма: {min + max}");
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        public static void SolveTask2()
        {
            Console.Clear();
            Console.WriteLine("Задание 2. Сумма чётных\n");

            string filePath = "task2.txt";
            FillFileMultipleNumbersPerLine(filePath, 6);

            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine($"Файл '{filePath}' заполнен случайными числами:\n");

            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine("  " + lines[i]);
                string[] tokens = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < tokens.Length; j++)
                {
                    int num = int.Parse(tokens[j]);
                    if (num % 2 == 0) sum += num;
                }
            }

            Console.WriteLine($"\nСумма чётных: {sum}");
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        public static void SolveTask3()
        {
            Console.Clear();
            Console.WriteLine("Задание 3. Первые символы строк\n");

            string inputPath = "task3_input.txt";
            string outputPath = "task3_output.txt";

            FillTextFileTask3(inputPath);

            string[] lines = File.ReadAllLines(inputPath);

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length > 0)
                        writer.WriteLine(lines[i][0]);
                    else
                        writer.WriteLine();
                }
            }

            Console.WriteLine($"Файл '{outputPath}' создан. Первые символы строк:");
            string[] outLines = File.ReadAllLines(outputPath);
            for (int i = 0; i < outLines.Length; i++)
                Console.WriteLine($"  '{outLines[i]}'");

            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        public static void SolveTask4()
        {
            Console.Clear();
            Console.WriteLine("Задание 4. Исключить кратные k\n");

            string inputPath = "task4_input.bin";
            string outputPath = "task4_output.bin";

            FillBinaryFile(inputPath, 15);
            Console.WriteLine($"Файл '{inputPath}' заполнен случайными числами (15 шт.).");

            int k = InputValidator.ReadIntNonZero("Введите k (целое, не ноль): ");

            List<int> original = new List<int>();
            List<int> filtered = new List<int>();

            using (BinaryReader reader = new BinaryReader(File.OpenRead(inputPath)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int num = reader.ReadInt32();
                    original.Add(num);
                    if (num % k != 0) filtered.Add(num);
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(outputPath)))
            {
                for (int i = 0; i < filtered.Count; i++)
                    writer.Write(filtered[i]);
            }

            Console.WriteLine($"\nИсходные: {string.Join(", ", original)}");
            Console.WriteLine($"Не кратные {k}: {(filtered.Count > 0 ? string.Join(", ", filtered) : "нет")}");
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        public static void SolveTask5()
        {
            Console.Clear();
            Console.WriteLine("Задание 5. Игрушки (XML), поиск для 3 лет\n");

            string filePath = "toys.xml";
            FillToysFile(filePath);
            Console.WriteLine($"Файл '{filePath}' создан (XML-сериализация).\n");

            XmlSerializer xml = new XmlSerializer(typeof(List<Toy>));
            List<Toy> toys;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                toys = (List<Toy>)xml.Deserialize(fs);
            }

            Console.WriteLine("Все игрушки:");
            for (int i = 0; i < toys.Count; i++)
                Console.WriteLine($"  {toys[i].Name} — {toys[i].Price} руб. (возраст {toys[i].MinAge}-{toys[i].MaxAge})");

            Console.WriteLine("\nПодходящие для 3 лет (кроме мяча):");
            bool found = false;
            for (int i = 0; i < toys.Count; i++)
            {
                Toy toy = toys[i];
                if (toy.Name.ToLower() != "мяч" && toy.MinAge <= 3 && toy.MaxAge >= 3)
                {
                    Console.WriteLine($"  {toy.Name} — {toy.Price} руб.");
                    found = true;
                }
            }

            if (!found) Console.WriteLine("  не найдено");
            Console.Write("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    public struct Toy
    {
        public string Name;
        public int Price;
        public int MinAge;
        public int MaxAge;
    }
}